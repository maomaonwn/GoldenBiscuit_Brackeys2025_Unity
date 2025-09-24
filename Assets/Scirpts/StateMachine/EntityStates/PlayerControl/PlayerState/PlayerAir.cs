namespace Scirpts.StateMachine.EntityStates.PlayerControl.PlayerState
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

            //空中移动朝向
            float jumpDir = player.facingDir;
            if (player.inputMoveVec2_X != 0)
                jumpDir = player.inputMoveVec2_X;
            //空中移动（85％速度）
            if(player.inputMoveVec2_X != 0)
                player.SetVelocity(player.moveSpeed*.85f*jumpDir,player.rb.velocity.y);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}