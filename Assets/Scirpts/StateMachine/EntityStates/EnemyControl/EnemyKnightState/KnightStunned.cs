using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    /// <summary>
    /// 被格挡后的短暂晕眩状态
    /// </summary>
    public class KnightStunned : EntityState
    {
        private Enemy_Knight enemy;
        public KnightStunned(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }
    }
}