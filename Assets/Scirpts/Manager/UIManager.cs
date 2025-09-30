using System;
using System.Collections;
using DG.Tweening;
using GameAssets.Plugins.Demigiant.DOTween.Modules;
using Scirpts.Base;
using Scirpts.Interaction;
using Scirpts.StateMachine.EntityStat;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scirpts.Manager
{
    /// <summary>
    /// update UI upon call
    /// UI component manager
    /// </summary>
    public class UIManager : SingletonBase<UIManager>
    {
        [Header("Inventory UI")]
        public InventoryData invData;
        public ItemType cookieItem; 
        private TMP_Text cookieCountText;   //饼干数量UI
        
        [Header("Health UI")]
        public EntityStat playerStat;
        private TMP_Text healthText;    //角色血量UI
        private int playerLastHealth;

        [Header("Boss UI")] 
        public BossStat bossStat;
        public Slider bossHealthSlider;

        public CanvasGroup bossUIGroup;
        public TMP_Text bossNameText;
        public TMP_Text bossDescText;

        //UI组件在场景中的名字
        private const string COOKIE_COUNT_TEXT_NAME = "CookieCounter";
        private const string HEALTH_TEXT_NAME = "HealthCounter";
        private const string BOSS_HEALTH_SLIDER_NAME = "HpLine_UI";
        private const string BOSS_UI_GROUP_NAME = "BossUIGroup";
        private const string BOSS_NAME_TEXT_NAME = "BossNameTMPText";
        private const string BOSS_DESC_TEXT_NAME = "BossDescTMPText";
        
        void OnEnable()
        {
            //注册跨场景处理事件
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            //UI事件的处理均在跨场景处理函数中完成了
        }

        /// <summary>
        /// 跨场景处理
        /// <remarks>在所有场景都重新处理了一次UI事件和UI组件，包含进入的第一个场景</remarks>
        /// </summary>
        /// <param name="_scene"></param>
        /// <param name="_mode"></param>
        public void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
        {
#if  UNITY_EDITOR
            Debug.Log($"Scene loaded: {_scene.name}. OnSceneLoaded Event Processing");
#endif
            // 1. 重新绑定UI组件
            RebindUIComponents();
            // 2. 重新订阅游戏内事件
            SubscribeUIEvents();
            // 3. 立即更新一次UI
            UpdateUIComponents();
        }

        private void Update()
        {
            //控制BossScene中的UI显示
            if(DialogManager.Instance.b_JustEndDialog)  //BossScene的对话结束时
                ShowBossUI();
        }

        void OnDisable()
        {
            //取消订阅跨场景处理事件
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
            //取消订阅UI事件
            UnsubscribeUIEvents();
        }
        
        #region UI事件方法
    
        void HandleItemChanged(ItemType type, int value)
        {
            if (type == cookieItem)
            {
                cookieCountText.text = $"Cookies: {value}";
            }
        }
    
        void HandleHealthChanged(int currentH, int maxH)
        {
            StartCoroutine(DigitalTransition_HealthChangeAnim(playerLastHealth, currentH, .3f));
        }

        void HandleBossHealthChanged(int currentH, int maxH)
        {
            bossHealthSlider.maxValue = maxH;
            bossHealthSlider.DOValue(currentH, .3f); //0.3f缓动
        }
        
        #endregion
    
        #region UI效果实现

        /// <summary>
        /// 数字渐变地更新文本
        /// </summary>
        /// <param name="_oldValue"></param>
        /// <param name="_newValue"></param>
        /// <param name="_animDuration"></param>
        /// <returns></returns>
        IEnumerator DigitalTransition_HealthChangeAnim(int _oldValue, int _newValue, float _animDuration)
        {
            float timer = 0f;

            while (timer < _animDuration)
            {
                timer += Time.deltaTime;

                int displayValue = Mathf.RoundToInt(Mathf.Lerp(_oldValue, _newValue, timer / _animDuration));
                
                //数字渐变动画
                healthText.text = $"Health: {displayValue.ToString()}";
                yield return null;
            }
            
            //最终血量
            healthText.text = $"Health: {_newValue.ToString()}";
        }

        /// <summary>
        /// 显现BossUI
        /// </summary>
        public void ShowBossUI()
        {
            bossUIGroup.DOFade(1f, 1f);    //UI组淡入

            // 初始化（在场景中配置）
            // uiGroup.alpha = 0f;
            // bossNameText.rectTransform.localScale = Vector3.zero;
            // bossDescText.rectTransform.localScale = Vector3.zero;
            
            // 淡入 + 缩放弹出
            bossUIGroup.DOFade(1f, 1f);
            bossNameText.rectTransform.DOScale(3.5f, .6f).SetEase(Ease.OutBack);
            bossDescText.rectTransform.DOScale(2.1f, .6f).SetEase(Ease.OutBack).SetDelay(0.3f);
        }
        
        #endregion

        #region 跨场景时的处理

        /// <summary>
        /// 跨场景时重新绑定UI组件
        /// <remarks>在新场景加载后动态查找并绑定UI组件</remarks>
        /// </summary>
        private void RebindUIComponents()
        {
            //查找饼干数量UI
            cookieCountText = GameObject.Find(COOKIE_COUNT_TEXT_NAME)?.GetComponent<TMP_Text>();
            //查找角色血量UI
            healthText = GameObject.Find(HEALTH_TEXT_NAME)?.GetComponent<TMP_Text>();
            //查找BossUI
            bossHealthSlider = GameObject.Find(BOSS_HEALTH_SLIDER_NAME)?.GetComponent<Slider>();
            bossNameText = GameObject.Find(BOSS_NAME_TEXT_NAME)?.GetComponent<TMP_Text>();
            bossDescText = GameObject.Find(BOSS_DESC_TEXT_NAME)?.GetComponent<TMP_Text>();
            bossUIGroup = GameObject.Find(BOSS_UI_GROUP_NAME)?.GetComponent<CanvasGroup>();
            
            //组件丢失报错
#if UNITY_EDITOR
            if(cookieCountText is null)Debug.LogError("CookieCountText component of HUD not found");
            if(healthText is null)Debug.LogError("HealthText component of HUD not found");
#endif
        }

        /// <summary>
        /// 订阅UI事件
        /// </summary>
        private void SubscribeUIEvents()
        {
            //先取消订阅，避免重复
            UnsubscribeUIEvents();

            if (invData is not null)
                invData.OnItemChanged += HandleItemChanged;
            if (playerStat is not null)
                playerStat.OnHealthChanged += HandleHealthChanged;
            if(bossStat is not null)
                bossStat.OnHealthChanged += HandleBossHealthChanged;
        }

        /// <summary>
        /// 取消订阅UI事件
        /// </summary>
        private void UnsubscribeUIEvents()
        {
            if (invData is not null)
                invData.OnItemChanged -= HandleItemChanged;
        
            if(playerStat is not null)
                playerStat.OnHealthChanged -= HandleHealthChanged;
            
            if(bossStat is not null)
                bossStat.OnHealthChanged -= HandleBossHealthChanged;
        }

        /// <summary>
        /// 更新UI模块 / 执行UI事件
        /// </summary>
        private void UpdateUIComponents()
        {
            if (invData is not null && cookieCountText is not null)
                HandleItemChanged(cookieItem, invData.GetAmount(cookieItem));

            if (playerStat is not null && healthText is not null)
            {
                playerLastHealth = playerStat.CurrentHealth;
                HandleHealthChanged(playerStat.CurrentHealth, playerStat.maxHealth);
            }
            
            if(bossStat is not null && bossHealthSlider is not null)
                HandleBossHealthChanged(bossStat.CurrentHealth, bossStat.maxHealth);
        }

        #endregion
    }
}