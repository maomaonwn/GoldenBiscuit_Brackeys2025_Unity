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
        
        public override void OnEnter()
        {
            base.OnEnter();

            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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

        void JumpTowardsPlayer()
        {
            if (!boss.IsGroundDetected()) return;
            
            // 获取方向（只考虑X方向，Y由跳跃决定）
            float directionX = (playerPos.position.x - boss.transform.position.x > 0) ? 1f : -1f;
            
            // 设置跳跃初速度
            float jumpX = directionX * boss.moveSpeed; // 横向速度
            float jumpY = boss.jumpForce;              // 竖直向上跳跃力

            boss.SetVelocity(jumpX, jumpY);
        }
    }
}