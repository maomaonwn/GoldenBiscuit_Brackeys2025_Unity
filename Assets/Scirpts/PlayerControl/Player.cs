using Scirpts.PlayerControl.PlayerState;


namespace Scirpts.PlayerControl
{
    public class Player : Entity
    {
        public StateMachine machine { get; private set; }
        public PlayerIdle idleState {get; private set;}
        public PlayerWalk walkState {get; private set;}
        public PlayerPrimaryAttack primaryAttack { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            machine = new StateMachine();
            idleState = new PlayerIdle(this, machine, "Idle", this);
            walkState = new PlayerWalk(this, machine, "Walk", this);
            primaryAttack = new PlayerPrimaryAttack(this, machine, "PrimaryAttack", this);
        }

        protected override void Start()
        {
            base.Start();
            
            //切入初始状态
            machine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();
            
            //状态的帧执行
            machine.currentState.OnUpdate();
        }
    }
}