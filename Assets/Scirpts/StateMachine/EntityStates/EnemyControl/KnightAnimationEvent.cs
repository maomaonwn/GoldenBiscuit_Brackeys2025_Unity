using Scirpts.APITool;
using Scirpts.StateMachine.EntityStat;
using Scirpts.StateMachine.EntityStates.PlayerControl;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.EnemyControl
{
    public class KnightAnimationEvent : MonoBehaviour
    {
        private Enemy_Knight enemy => GetComponentInParent<Enemy_Knight>();

        /// <summary>
        /// 通用动画触发函数
        /// </summary>
        private void AnimationTrigger() => enemy.AnimationTrigger();
        
        /// <summary>
        /// 攻击判定函数
        /// <remarks>此函数可以判定是否击中玩家，如击中，玩家受伤</remarks>
        /// </summary>
        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position,enemy.attackCheckRadius);

            foreach(var hit in colliders)
            {
                if(hit.GetComponent<Player>()!= null)
                {
                    hit.GetComponent<Player>().Damage();
                    hit.GetComponent<PlayerStat>().TakeDamage(enemy.enemyStat.damage);
                }
            }
        }

        /// <summary>
        /// 当死亡动画结束时
        /// </summary>
        private void OnDeathAnimationEnd()
        {
            DelayCallTool.Call(2f, () =>
            {
                Debug.Log("2秒后执行！");
                    Destroy(gameObject);
            });
        }
        
        private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
    }
}