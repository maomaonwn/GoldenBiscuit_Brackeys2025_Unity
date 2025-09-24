using GameAssets.Plugins.Demigiant.DOTween.Modules;
using Scirpts.StateMachine.EntityStates.BossControl;
using UnityEngine.UI;

namespace Scirpts.StateMachine.EntityStat
{
    public class BossStat : EntityStat
    {
        private Boss boss => GetComponent<Boss>();

        public Slider slider;

        public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);
            
            UpdateHealthUI(CurrentHealth,maxHealth);
        }

        public void UpdateHealthUI(int _currentHealth, int _maxHealth)
        {
            slider.maxValue = _maxHealth;
            slider.DOValue(CurrentHealth, .3f); //0.3f缓动
        }
    }
}