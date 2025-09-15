using System.Collections;
using Scirpts.PlayerControl;
using TMPro;
using UnityEngine;

namespace Scirpts.EntityStat
{
    public class PlayerStat : EntityStat
    {
        Player player => GetComponent<Player>();

        public TMP_Text healthText;
        
        public override void Die()
        {
            base.Die();
            
            //->Dead 切换到死亡状态 
            player.Die();
        }

        public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);
            
            //更新血量UI
            UpdateHealthUI(_damage);
        }

        /// <summary>
        /// 更新血量 UI 
        /// </summary>
        private void UpdateHealthUI(int _damage)
        {
            StartCoroutine(HealthChangeAnim(CurrentHealth, CurrentHealth - _damage, .3f));
        }

        /// <summary>
        /// 数字渐变地更新文本
        /// </summary>
        /// <param name="_oldValue"></param>
        /// <param name="_newValue"></param>
        /// <param name="_animDuration"></param>
        /// <returns></returns>
        IEnumerator HealthChangeAnim(int _oldValue, int _newValue,float _animDuration)
        {
            float timer = 0f;

            while (timer < _animDuration)
            {
                timer += Time.deltaTime;
                int displayValue = Mathf.RoundToInt(Mathf.Lerp(_oldValue, _newValue, timer / _animDuration));

                //更新血量文本
                healthText.text = $"Health: {displayValue.ToString()}";
                yield return null;
            }

            healthText.text = $"Health: {_newValue.ToString()}";
        }
    }
}