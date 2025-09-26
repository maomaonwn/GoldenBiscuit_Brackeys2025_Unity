using System;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStat
{
    public class EntityStat : MonoBehaviour
    {
        [SerializeField]public int maxHealth;
        public int damage;
        
        [SerializeField]public int currentHealth;
        
        public event Action<int,int> OnHealthChanged;// <currentHealth, maxHealth>
        
        public int CurrentHealth
        {
            get { return currentHealth; }
            private set { currentHealth = Mathf.Max(value,0); } //保证不为负值
        }
        
        private void Awake()
        {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// 血量减少
        /// </summary>
        /// <param name="_damage">伤害值</param>
        public virtual void TakeDamage(int _damage)
        {
            currentHealth -= _damage;
            
            // Update UI
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
            
            if(currentHealth <= 0)
                Die();
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public virtual void Die()
        {
            
        }

        /// <summary>
        /// 回血
        /// </summary>
        /// <param name="_healAmount">血量增加量</param>
        public void Heal(int _healAmount)
        {
            if (_healAmount > 0)
            {
                CurrentHealth = Mathf.Min(CurrentHealth + _healAmount, maxHealth);  //保证不会超过最大血量
                // Update UI
                OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
            }
        }
    }
}