namespace Scirpts.StateMachine.EntityStates.PlayerControl.PlayerState
{
    public class PlayerDash : EntityState
    {
        private Player player;
        public PlayerDash(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateTimer = player.dashDuration;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            player.SetVelocity(player.dashSpeed*player.dashDir,0);
            
            //->Idle
            if(stateTimer < 0)
                machine.ChangeState(player.idleState);
        }

        public override void OnExit()
        {
            base.OnExit();
            
            player.SetVelocity(0,player.rb.velocity.y);
        }
    }
}