using Scirpts.Manager;

namespace Scirpts.StateMachine.EntityStates.PlayerControl.PlayerState
{
    public class PlayerWalk : PlayerGrounded
    {
        private Player player;
        public PlayerWalk(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName,_entity)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //播放音效
            AudioManager.instance.PlaySoundEffect(SoundEffectName.Player_Walk,true);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            player.SetVelocity(player.inputMoveVec2_X * player.moveSpeed,player.rb.velocity.y);
            
            //->Idle
            if (player.inputMoveVec2_X == 0)
                machine.ChangeState(player.idleState);
        }

        public override void OnExit()
        {
            base.OnExit();
            
            //停止播放音效
            AudioManager.instance.StopSoundEffect();
        }
    }
}