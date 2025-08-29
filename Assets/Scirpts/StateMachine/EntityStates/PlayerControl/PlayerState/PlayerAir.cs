using UnityEngine;

namespace Scirpts.PlayerControl.PlayerState
{
    public class PlayerAir : EntityState
    {
        private Player player;
        public PlayerAir(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Idle
            if(player.IsGroundDetected())
                machine.ChangeState(player.idleState);
            
            //->空中移动（80％的速度）
            if(player.inputMoveVec2_X != 0)
                player.SetVelocity(player.moveSpeed * .8f *player.inputMoveVec2_X , player.rb.velocity.y);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}