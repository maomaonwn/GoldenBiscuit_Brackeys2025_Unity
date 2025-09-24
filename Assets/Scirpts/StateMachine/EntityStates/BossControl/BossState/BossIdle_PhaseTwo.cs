using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.BossControl.BossState
{
    public class BossIdle_PhaseTwo : EntityState
    {
        private Boss boss;
        public BossIdle_PhaseTwo(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            stateTimer = Random.Range(boss.CurrentValues.minIdleTime,boss.CurrentValues.maxIdleTime);
            
            Debug.Log("进入 Idle_PhaseTwo 状态");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->JumpAttack_PhaseTwo
            if(stateTimer < 0)
                machine.ChangeState(boss.jumpAttackState_PhaseTwo);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}