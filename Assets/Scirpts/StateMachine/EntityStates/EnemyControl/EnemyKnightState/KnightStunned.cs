using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.EnemyControl.EnemyKnightState
{
    /// <summary>
    /// 被格挡后的短暂晕眩状态
    /// </summary>
    public class KnightStunned : EntityState
    {
        private Enemy_Knight enemy;
        public KnightStunned(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            enemy.fx.InvokeRepeating("RedColorBlink",0,.1f);

            //初始化
            //晕眩时间
            stateTimer = enemy.stunnedDuration;
            //晕眩时发生的位移  (没flip -> 没用SetVelocity())
            enemy.rb.velocity = new Vector2(-enemy.facingDir * enemy.stunnedPower.x , enemy.stunnedPower.y);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            //->Idle
            if(stateTimer < 0)
                machine.ChangeState(enemy.idleState);
        }
        public override void OnExit()
        {
            base.OnExit();

            enemy.fx.Invoke("CancelRedBlink",0);
        }
    }
}