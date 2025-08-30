using Scirpts.PlayerControl;
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
    }
}