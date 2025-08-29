using UnityEngine;

namespace Scirpts.EntityStates.BOSSControl.BossState
{
    public class BOSSJump : EntityState
    {
        private BOSS boss;
        public BOSSJump(Entity _entityBase, StateMachine _machine, string _animBoolName,BOSS _entity) : base(_entityBase, _machine, _animBoolName)
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
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}