using DG.Tweening;
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
        public BossIntroOne introOneState { get; private set; }
        public BossIntroTwo introTwoState { get; private set; }
        
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
            introOneState = new BossIntroOne(this, machine, "JumpAttack", this);
            introTwoState = new BossIntroTwo(this, machine, "PhaseTwo", this);
        }
        
        protected override void Start()
        {
            base.Start();
            //初始化状态
            machine.Initialize(introOneState);
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
        protected void CheckForPhaseChange()
        {
            //->PhaseTwo
            if (bossStat.CurrentHealth <= 0.35f * bossStat.maxHealth && !b_intoPhaseTwo)
            {
                machine.ChangeState(introTwoState);
            }
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !DialogManager.Instance.isDialogueActive)
            {
                // --------------------
                // 2. 落地视觉效果
                // --------------------
                //屏幕震动
                Camera.main.DOShakePosition(
                    duration: .3f,  //持续时间（秒）
                    strength: .5f,  //抖动强度
                    vibrato: 10,    //抖动次数
                    randomness: 90, //抖动随机度
                    fadeOut: true   //是否渐渐减弱
                );
            }
        }
    }
}