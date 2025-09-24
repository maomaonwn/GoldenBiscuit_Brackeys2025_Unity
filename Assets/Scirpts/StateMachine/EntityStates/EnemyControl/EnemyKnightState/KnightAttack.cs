using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.EnemyControl.EnemyKnightState
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    public class KnightAttack : EntityState
    {
        private Enemy_Knight enemy;
        public KnightAttack(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
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
            
            enemy.SetZeroVelocity();
            
            //->Battle
            if(b_TriggerCalled)
                machine.ChangeState(enemy.battleState);
        }

        public override void OnExit()
        {
            base.OnExit();
            
            enemy.lastTimeAttacked = Time.time;
        }
    }
}