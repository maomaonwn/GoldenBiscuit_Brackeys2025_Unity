using Scirpts.EntityStat;
using Scirpts.EntityStates.BossControl.BossState;
using UnityEngine;

namespace Scirpts.EntityStates.BossControl
{
    public class Boss : Entity
    {
        //状态
        public BossIdle idleState { get; private set; }
        public BossJumpAttack jumpAttackState { get; private set; }
        public BossPhaseTwo phaseTwoState { get; private set; }
        
        public BossStat bossStat { get; private set; }

        [Header("战斗")]
        public float minIdleTime = .5f;
        public float maxIdleTime = 1.5f;
        public float minJumpTime = .3f;
        public float maxJumpTime = 1.2f;

        public float jumpForce;

        protected bool b_intoPhaseTwo;

        protected override void Awake()
        {
            base.Awake();

            bossStat = GetComponent<BossStat>();
            
            //状态实例
            idleState = new BossIdle(this, machine, "Idle", this);
            jumpAttackState = new BossJumpAttack(this, machine, "JumpAttack", this);
            phaseTwoState = new BossPhaseTwo(this, machine, "PhaseTwo", this);
        }
        
        protected override void Start()
        {
            base.Start();
            //初始化状态
            machine.Initialize(idleState);
        }
        
        protected override void Update()
        {
            base.Update();
            //状态的帧执行
            machine.currentState.OnUpdate();
            
            CheckForPhaseChange();
        }

        /// <summary>
        /// 检测是否到达二阶段
        /// </summary>
        void CheckForPhaseChange()
        {
            //->PhaseTwo
            if (bossStat.CurrentHealth <= 0.35f * bossStat.maxHealth && !b_intoPhaseTwo)
            {
                machine.ChangeState(phaseTwoState);
            }
        }

        void IsGroundDetected() => IsGroundDetected();
    }
}