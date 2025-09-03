using Scirpts.PlayerControl;
using TMPro;
using UnityEngine;

namespace Scirpts.EntityStat
{
    public class PlayerStat : EntityStat
    {
        Player player => GetComponent<Player>();
        
        public override void Die()
        {
            base.Die();
            
            //->Dead 切换到死亡状态 
            player.Die();
        }
        
        /// <summary>
        /// 更新 UI 文本
        /// </summary>
        private void UpdateHealthUI()
        {
            //UI更新相关逻辑用UI Manager管理
            // logic related to ui update is in "UIManager" Script
        }
    }
}