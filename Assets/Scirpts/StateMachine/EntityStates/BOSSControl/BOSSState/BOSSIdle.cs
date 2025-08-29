using UnityEngine;

namespace Scirpts.EntityStates.BOSSControl.BossState
{
    public class BOSSIdle : EntityState
    {
        private BOSS boss;
        public BOSSIdle(Entity _entityBase, StateMachine _machine, string _animBoolName,BOSS _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            stateTimer = Random.Range(boss.minTime, boss.maxTime);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Jump
            if(stateTimer <= 0)
                machine.ChangeState(boss.jumpState);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}