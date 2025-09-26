using System.Collections;
using DG.Tweening;
using Scirpts.Base;
using Scirpts.Interaction;
using Scirpts.StateMachine.EntityStat;
using TMPro;
using UnityEngine;
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
        public TMP_Text cookieCount;
        public ItemType cookieItem; 

        [Header("Health UI")]
        public EntityStat playerStat;
        public TMP_Text healthText;
        private int playerLastHealth;

        [Header("Boss UI")] 
        public BossStat bossStat;
        public Slider bossHealthSlider;
        
        void OnEnable()
        {
            if (invData != null)
                invData.OnItemChanged += HandleItemChanged;
            if (playerStat != null)
                playerStat.OnHealthChanged += HandleHealthChanged;
            if(bossStat != null)
                bossStat.OnHealthChanged += HandleBossHealthChanged;
        }

        void Start()
        {
            if (invData != null)
                HandleItemChanged(cookieItem,invData.GetAmount(cookieItem));

            if (playerStat != null)
            {
                playerLastHealth = playerStat.CurrentHealth;
                HandleHealthChanged(playerStat.CurrentHealth, playerStat.maxHealth);
            }
            
            if(bossStat != null)
                HandleBossHealthChanged(bossStat.CurrentHealth,bossStat.maxHealth);
        }

        void OnDisable()
        {
            if (invData != null)
                invData.OnItemChanged -= HandleItemChanged;
        
            if(playerStat!=null)
                playerStat.OnHealthChanged -= HandleHealthChanged;
            
            if(bossStat != null)
                bossStat.OnHealthChanged -= HandleBossHealthChanged;
        }

        void HandleItemChanged(ItemType type, int value)
        {
            if (type == cookieItem)
            {
                cookieCount.text = $"Cookies: {value}";
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
    }
}