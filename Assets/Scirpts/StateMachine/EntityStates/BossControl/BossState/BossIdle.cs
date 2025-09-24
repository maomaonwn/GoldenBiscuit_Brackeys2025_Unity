using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.BossControl.BossState
{
    public class BossIdle : EntityState
    {
        private Boss boss;
        public BossIdle(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateTimer = Random.Range(boss.CurrentValues.minIdleTime,boss.CurrentValues.maxIdleTime);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if(stateTimer < 0)
                machine.ChangeState(boss.jumpAttackState);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}