using UnityEngine;

namespace Scirpts.PlayerControl.PlayerState
{
    public class PlayerIdle : PlayerGrounded
    {
        private Player player;
        
        public PlayerIdle(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName,_entity)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            player.SetZeroVelocity();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Walk
            if (player.inputMoveVec2_X != 0)
            {
                machine.ChangeState(player.walkState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}