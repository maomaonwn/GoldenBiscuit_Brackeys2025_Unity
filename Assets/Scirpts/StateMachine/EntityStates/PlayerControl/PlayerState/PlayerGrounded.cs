namespace Scirpts.StateMachine.EntityStates.PlayerControl.PlayerState
{
    public class PlayerGrounded : EntityState
    {
        private Player player;
        public PlayerGrounded(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
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
            
            //->PrimaryAttack
            if(player.inputSystem.Gameplay.Attack.triggered)
                machine.ChangeState(player.primaryAttack);
            
            //->Jump
            if(player.inputSystem.Gameplay.Jump.triggered)
                machine.ChangeState(player.jumpState);
            
            // //->CounterAttack
            // if(player.inputSystem.Gameplay.CounterAttack.triggered)
            //     machine.ChangeState(player.counterAttack);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}