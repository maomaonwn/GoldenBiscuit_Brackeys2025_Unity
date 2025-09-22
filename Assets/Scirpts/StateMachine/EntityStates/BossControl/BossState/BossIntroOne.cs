using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;

namespace Scirpts.EntityStates.BossControl.BossState
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
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            boss.StartCoroutine(DelayChangeState());
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