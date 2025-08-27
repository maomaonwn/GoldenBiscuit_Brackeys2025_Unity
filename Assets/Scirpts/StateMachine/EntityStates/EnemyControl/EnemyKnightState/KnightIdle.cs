using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    public class KnightIdle : EntityState
    {
        private Enemy_Knight enemy;
        public KnightIdle(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateTimer = enemy.idleTime;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Move
            if(stateTimer < 0)
                machine.ChangeState(enemy.moveState);
            //->Battle+
            if(enemy.IsPlayerDetected())
                machine.ChangeState(enemy.battleState);
                
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}