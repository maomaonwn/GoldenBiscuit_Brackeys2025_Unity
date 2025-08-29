using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    /// <summary>
    /// 交战逻辑（过渡状态）
    /// </summary>
    public class KnightBattle : EntityState
    {
        private Enemy_Knight enemy;
        public KnightBattle(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }
        
        
    }
}