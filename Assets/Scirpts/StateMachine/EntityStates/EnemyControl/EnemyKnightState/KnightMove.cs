using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    public class KnightMove : KnightGrounded
    {
        private Enemy_Knight enemy;
        public KnightMove(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName,_entity)
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
            
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir,enemy.rb.velocity.y);
            
            //->Idle
            if (!enemy.IsGroundDetected())
            {
                enemy.Flip();
                machine.ChangeState(enemy.idleState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}