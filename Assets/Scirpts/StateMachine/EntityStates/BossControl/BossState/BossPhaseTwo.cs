using UnityEngine;

namespace Scirpts.EntityStates.BossControl.BossState
{
    public class BossPhaseTwo : EntityState
    {
        private Boss boss;
        public BossPhaseTwo(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }
    }
}