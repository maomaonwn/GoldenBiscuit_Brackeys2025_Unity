using System.Collections;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.BossControl.BossState
{
    public class BossIntroOne : EntityState
    {
        private Boss boss;
        public BossIntroOne(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            boss.StartCoroutine(DelayChangeState());
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private IEnumerator DelayChangeState()
        {
            yield return new WaitForSeconds(1.5f); //出场状态表演时间
            
            //->Idle
            machine.ChangeState(boss.idleState);
        }
    }
}