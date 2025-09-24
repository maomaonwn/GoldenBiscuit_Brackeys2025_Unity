using System.Collections;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.BossControl.BossState
{
    public class BossIntroTwo : EntityState
    {
        private Boss boss;
        public BossIntroTwo(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            boss.currentPhase = 2;
            boss.StartCoroutine(DelayChangeState());
            
            Debug.Log("进入 IntroTwo 状态");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        
        private IEnumerator DelayChangeState()
        {
            yield return new WaitForSeconds(1f); //出场状态表演时间
            
            //->Idle_PhaseTwo
            machine.ChangeState(boss.idleState_PhaseTwo);
        }
    }
}