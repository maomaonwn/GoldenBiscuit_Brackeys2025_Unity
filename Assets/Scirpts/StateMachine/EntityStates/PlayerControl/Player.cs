using Scirpts.PlayerControl.PlayerState;
using UnityEngine;


namespace Scirpts.PlayerControl
{
    public class Player : Entity
    {
        //状态
        public PlayerIdle idleState {get; private set;}
        public PlayerWalk walkState {get; private set;}
        public PlayerPrimaryAttack primaryAttack { get; private set; }
        public PlayerJump jumpState { get; private set; }
        public PlayerAir airState { get; private set; }
        public PlayerDash dashState { get; private set; }

        public float jumpForce;
        
        [Header("攻击")] 
        public Vector2[] attackMovement;

        [Header("冲刺")] 
        public float dashSpeed;
        public float dashDuration = .2f;
        public float dashDir { get; private set; }
        public bool b_CanDash = false;
        
        protected override void Awake()
        {
            base.Awake();
            
            idleState = new PlayerIdle(this, machine, "Idle", this);
            walkState = new PlayerWalk(this, machine, "Walk", this);
            primaryAttack = new PlayerPrimaryAttack(this, machine, "PrimaryAttack", this);
            jumpState = new PlayerJump(this, machine, "Jump", this);
            airState = new PlayerAir(this, machine, "Fall", this);
            dashState = new PlayerDash(this, machine, "Dash", this);
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
            
            //(Player)全局的冲刺检测
            if(b_CanDash)
                CheckForDashInput();
        }

        #region Dash

        /// <summary>
        /// 切换进冲刺（Dash）状态
        /// </summary>
        private void CheckForDashInput()    //在Player实例脚本中写，能实现在任意状态下切换进冲刺状态
        {
            if (inputSystem.Gameplay.Dash.triggered)
            {
                //冲刺朝向 由水平输入决定
                dashDir = inputMoveVec2_X;  //多一个单独的dashDir，能使玩家有更灵活的操作空间，比如说静止时也可以选择冲刺的方向
                if (dashDir == 0)
                    dashDir = facingDir;
                
                //->Dash
                machine.ChangeState(dashState);
            }
        }

        #endregion
    }
}