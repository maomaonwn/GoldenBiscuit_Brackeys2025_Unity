using Scirpts.EntityStates.BOSSControl.BossState;
using Unity.VisualScripting;
using UnityEngine;

namespace Scirpts.EntityStates.BOSSControl
{
    public class BOSS : Entity
    {
        public int health;
        public int damage;
        private float timeBtwDamage = 1.5f;

        // public Animator camAnim;
        public bool b_isDead { get; private set; }

        public float minTime;
        public float maxTime;
        
        //状态
        public BOSSIntroStageOne introStageOne { get;private set; }
        public BOSSIdle idleState {get;private set;}
        public BOSSJump jumpState {get;private set;}
        
        
        protected override void Awake()
        {
            base.Awake();
            //状态实例
            introStageOne = new BOSSIntroStageOne(this, machine, "IntroStageOne", this);
            idleState = new BOSSIdle(this, machine, "IdleOne", this);
            jumpState = new BOSSJump(this, machine, "JumpOne", this);
        }

        protected override void Start()
        {
            base.Start();
            //初始化状态
            machine.Initialize(introStageOne);
        }

        protected override void Update()
        {
            base.Update();
            //状态的帧执行
            machine.currentState.OnUpdate();
        }
    }
}