using Scirpts;
using Scirpts.EntityStates.EnemyControl;
using Scirpts.PlayerControl;
using UnityEngine;

namespace Scirpts.PlayerControl.PlayerState
{
    public class PlayerCounterAttack : EntityState
    {
        private Player player;
        public PlayerCounterAttack(Entity _entityBase, StateMachine _FSMManager, string _animBoolName,
            Player _entity) : base(_entityBase, _FSMManager, _animBoolName)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateTimer = player.counterAttackDuration;
            player.anim.SetBool("SuccessfulCounterAttack", false);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            player.SetZeroVelocity();

            //登记攻击范围内的所有Collider
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy_Knight>() != null)
                    if (hit.GetComponent<Enemy_Knight>().EnterStunned()) //Enemy->Stunned
                    {
                        //任意比1大的值 （此时状态的退出交给了b_TriggerCalled）
                        stateTimer = 10;
                        player.anim.SetBool("SuccessfulCounterAttack", true);
                    }
            }

            //->Idle
            //如果超时(反击失败)，或者反击成功，则退出状态
            if (stateTimer < 0 || b_TriggerCalled)
            {
                machine.ChangeState(player.idleState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}