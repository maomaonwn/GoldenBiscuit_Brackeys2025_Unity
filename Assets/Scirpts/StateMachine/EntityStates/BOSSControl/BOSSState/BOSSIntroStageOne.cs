using UnityEngine;

namespace Scirpts.EntityStates.BOSSControl.BossState
{
    public class BOSSIntroStageOne : EntityState
    {
        private BOSS boss;
        public BOSSIntroStageOne(Entity _entityBase, StateMachine _machine, string _animBoolName,BOSS _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                //->Idle (chance:1/3)
                machine.ChangeState(boss.idleState);   
            }
            else
            {
                //->Jump (chance:2/3)
                machine.ChangeState(boss.jumpState);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}