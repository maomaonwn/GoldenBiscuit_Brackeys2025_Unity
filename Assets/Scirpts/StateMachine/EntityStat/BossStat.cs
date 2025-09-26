using System;
using DG.Tweening;
using Scirpts.StateMachine.EntityStates.BossControl;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Scirpts.StateMachine.EntityStat
{
    public class BossStat : EntityStat
    {
        private Boss boss => GetComponent<Boss>();

        public override void Die()
        {
            base.Die();
            
            //->Dead 切换到死亡状态
            boss.Die();
        }
    }
}