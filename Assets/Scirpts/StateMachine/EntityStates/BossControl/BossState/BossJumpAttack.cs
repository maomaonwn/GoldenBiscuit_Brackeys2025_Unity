using Unity.VisualScripting;
using UnityEngine;

namespace Scirpts.EntityStates.BossControl.BossState
{
    public class BossJumpAttack : EntityState
    {
        private Boss boss;
        public BossJumpAttack(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        private Transform playerPos;

        private float jumpForce;
        
        public override void OnEnter()
        {
            base.OnEnter();

            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            jumpForce = boss.jumpForce;
            
            stateTimer = Random.Range(boss.minJumpTime, boss.maxJumpTime);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if(stateTimer < 0)
                machine.ChangeState(boss.idleState);
            
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