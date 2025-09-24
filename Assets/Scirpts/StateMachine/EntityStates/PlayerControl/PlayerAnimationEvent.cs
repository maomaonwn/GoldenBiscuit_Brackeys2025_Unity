using Scirpts.StateMachine.EntityStat;
using Scirpts.StateMachine.EntityStates.BossControl;
using Scirpts.StateMachine.EntityStates.EnemyControl;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.PlayerControl
{
    /// <summary>
    /// 玩家动画事件方法
    /// </summary>
    public class PlayerAnimationEvent : MonoBehaviour
    {
        private Player player => GetComponentInParent <Player>();

        /// <summary>
        /// 通用动画触发函数
        /// </summary>
        private void AnimationTrigger() => player.AnimationTrigger();
        
        /// <summary>
        /// 攻击判定函数
        /// <remarks>此函数可以判定是否击中敌人，如击中，敌人受伤</remarks>
        /// </summary>
        private void AttackTrigger()
        {
            //登记攻击范围内的所有Collider
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);

            foreach(var hit in colliders)
            {
                //Enemy_Knight
                if(hit.GetComponent<Enemy_Knight>() is not null)
                {
                    hit.GetComponent<Enemy_Knight>().Damage();
                    hit.GetComponent<KnightStat>().TakeDamage(player.playerStat.damage);
                }

                //BOSS
                if (hit.GetComponent<Boss>() is not null)
                {
                    hit.GetComponent<Boss>().Damage();
                    hit.GetComponent<BossStat>().TakeDamage(player.playerStat.damage);
                }
            }
        }
    }
}