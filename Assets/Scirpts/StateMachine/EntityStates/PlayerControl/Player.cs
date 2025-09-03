using System.Collections;
using Scirpts.EntityStat;
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
        public PlayerDead deadState { get; private set; }
        // public PlayerCounterAttack counterAttack { get; private set; }

        public float jumpForce;
        
        [Header("攻击")] 
        public Vector2[] attackMovement;
        public float counterAttackDuration;
        
        [Header("冲刺")] 
        public float dashSpeed;
        public float dashDuration = .2f;
        public float dashDir { get; private set; }
        public float dashCooldown;
        public float dashCooldownTimer { get;private set; }
        public bool b_CanDash;

        public bool b_BeBusy { get; private set; }
        
        //PlayerStat
        public PlayerStat playerStat { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            
            idleState = new PlayerIdle(this, machine, "Idle", this);
            walkState = new PlayerWalk(this, machine, "Walk", this);
            primaryAttack = new PlayerPrimaryAttack(this, machine, "PrimaryAttack", this);
            jumpState = new PlayerJump(this, machine, "Jump", this);
            airState = new PlayerAir(this, machine, "Fall", this);
            dashState = new PlayerDash(this, machine, "Dash", this);
            deadState = new PlayerDead(this, machine, "Idle", this);   //deadState没有动画，使用一种程序特效表示，所以这里的动画值随便填
            // counterAttack = new PlayerCounterAttack(this, machine, "CounterAttack", this);
            
            playerStat = GetComponent<PlayerStat>();
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
            
            dashCooldownTimer -= Time.deltaTime;
        }

        #region Dash

        /// <summary>
        /// 切换进冲刺（Dash）状态
        /// </summary>
        private void CheckForDashInput()    //在Player实例脚本中写，能实现在任意状态下切换进冲刺状态
        {
            if (inputSystem.Gameplay.Dash.triggered && DashCoolDown())
            {
                //冲刺朝向 由水平输入决定
                dashDir = inputMoveVec2_X;  //多一个单独的dashDir，能使玩家有更灵活的操作空间，比如说静止时也可以选择冲刺的方向
                if (dashDir == 0)
                    dashDir = facingDir;
                
                //->Dash
                machine.ChangeState(dashState);
            }
        }

        /// <summary>
        /// 冲刺冷却
        /// </summary>
        /// <returns>返回true表示冷却完毕，返回false表示尚在冷却</returns>
        private bool DashCoolDown()
        {
            if (dashCooldownTimer < 0)
            {
                dashCooldownTimer = dashCooldown;
                return true;
            }
            
            #if UNITY_EDITOR
            Debug.Log("冲刺正在冷却");
            #endif

            return false;
        }

        #endregion

        #region Dead

        /// <summary>
        /// ->Dead 切换到死亡状态 
        /// </summary>
        public override void Die()
        {
            base.Die();
            
            machine.ChangeState(deadState);
        }

        #endregion
        
        /// <summary>
        /// 细节控制状态间的过渡
        /// </summary>
        /// <remarks>该函数可以使b_BeBusy值在指定时间内变为true，b_BeBusy值为true时表示有状态正在进行，是占用状态的</remarks>
        /// <param name="_seconds">在_seconds秒后，b_BeBusy恢复为false</param>
        /// <returns></returns>
        public IEnumerator BusyFor(float _seconds)
        {
            b_BeBusy = true;

            yield return new WaitForSeconds(_seconds);

            b_BeBusy = false;
        }
    }
}