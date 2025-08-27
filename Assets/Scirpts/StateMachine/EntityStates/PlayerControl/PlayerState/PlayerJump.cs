using UnityEngine;

namespace Scirpts.PlayerControl.PlayerState
{
    public class PlayerJump : EntityState
    {
        private Player player;
        public PlayerJump(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            //给予y轴的速度
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Air
            if(player.rb.velocity.y < 0)
                machine.ChangeState(player.airState);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}