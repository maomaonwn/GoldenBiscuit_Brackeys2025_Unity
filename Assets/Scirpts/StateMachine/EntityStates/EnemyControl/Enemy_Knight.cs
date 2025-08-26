using Scirpts.EntityStates.EnemyControl.EnemyKnightState;
using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl
{
    public class Enemy_Knight : Enemy
    {
        //状态
        public KnightIdle idleState { get;private set; }
        public KnightMove moveState { get; private set; }
        public KnightAttack attackState { get; private set; }
        public KnightStunned stunnedState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            //状态实例
            idleState = new KnightIdle(this, machine, "Idle", this);
            moveState = new KnightMove(this, machine, "Move", this);
            attackState = new KnightAttack(this, machine, "Attack", this);
            stunnedState = new KnightStunned(this, machine, "Stunned", this);
        }

        protected override void Start()
        {
            base.Start();
            //初始化状态
            machine.Initialize(idleState);
        }
    }
}