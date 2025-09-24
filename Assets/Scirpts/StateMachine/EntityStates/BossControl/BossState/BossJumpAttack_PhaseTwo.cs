using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.BossControl.BossState
{
    public class BossJumpAttack_PhaseTwo : EntityState
    {
        private Boss boss;
        public BossJumpAttack_PhaseTwo(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        private Transform playerPos;

        private float jumpForce;
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            //获取玩家位置
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            //获取跳跃力
            jumpForce = boss.jumpForce;
            
            stateTimer = Random.Range(boss.CurrentValues.minJumpTime,boss.CurrentValues.maxJumpTime);
            
            Debug.Log("进入 JumpAttack_PhaseTwo 状态");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Idle_PhaseTwo
            if(stateTimer < 0)
                machine.ChangeState(boss.idleState_PhaseTwo);
            
            JumpTowardsPlayer();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
        
        /// <summary>
        /// 跳跃向玩家
        /// </summary>
        void JumpTowardsPlayer()
        {
            if (!boss.IsGroundDetected())return;
            
            // --------------------
            // 1. 基础参数
            // --------------------
            //获取重力加速度
            float gravity = Physics2D.gravity.magnitude;
            //估算空中停留的时间 
            float timeInAir = (2 * jumpForce) / gravity;  //自由落地运动总飞行时间公式 : t = (2 * Vy) / g
            
            // --------------------
            // 2. 计算水平速度
            // --------------------
            float distanceX = playerPos.position.x - boss.transform.position.x;
            //计算水平速度
            float jumpX = distanceX / timeInAir;
            
            //给水平速度增加随机性
            jumpX += Random.Range(-1f, 1f);
            
            // --------------------
            // 3. 竖直速度固定
            // --------------------
            float jumpY = jumpForce;
            
            // --------------------
            // 4. 设置速度
            // --------------------
            boss.SetVelocity(jumpX, jumpY);
        }
    }
}