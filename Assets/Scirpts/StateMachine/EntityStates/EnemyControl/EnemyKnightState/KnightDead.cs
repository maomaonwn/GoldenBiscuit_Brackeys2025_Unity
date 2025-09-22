using DG.Tweening;
using Scirpts.APITool;
using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    public class KnightDead : EntityState
    {
        private Enemy_Knight enemy;
        public KnightDead(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
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