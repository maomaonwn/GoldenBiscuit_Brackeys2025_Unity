using System.Collections;
using Scirpts.StateMachine.EntityStates.PlayerControl;
using TMPro;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStat
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