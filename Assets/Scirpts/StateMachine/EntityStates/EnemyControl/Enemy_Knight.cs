using Scirpts.EntityStates.EnemyControl.EnemyKnightState;
using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl
{
    public class Enemy_Knight : Entity
    {
        //状态
        public KnightIdle idleState { get;private set; }
        public KnightMove moveState { get; private set; }
        public KnightBattle battleState { get; private set; }
        public KnightAttack attackState { get; private set; }
        public KnightStunned stunnedState { get; private set; }

        public float idleTime;
        
        [Header("碰撞体检测")]
        public Transform playerCheck;
        public LayerMask whatIsPlayer;
        public Transform attackCheck;
        public float attackCheckRadius;
        [Header("攻击")]
        public float attackDistance = 1.5f;    //最远攻击距离
        public float attackCooldown = .6f;
        [HideInInspector]public float lastTimeAttacked;
        [Tooltip("追击持续时间")]
        public float battleTime = 7f;    //追击持续时间

        protected override void Awake()
        {
            base.Awake();
            //状态实例
            idleState = new KnightIdle(this, machine, "Idle", this);
            moveState = new KnightMove(this, machine, "Move", this);
            battleState = new KnightBattle(this, machine, "Move", this);
            attackState = new KnightAttack(this, machine, "Attack", this);
            stunnedState = new KnightStunned(this, machine, "Stunned", this);
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
        }

        #region CollisionCheck

        /// <summary>
        /// Player检测
        /// </summary>
        /// <returns></returns>
        public virtual RaycastHit2D IsPlayerDetected() =>
            Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

        #endregion

        /// <summary>
        /// 可视化Debug
        /// </summary>
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            #region 最远攻击距离

            Gizmos.color = Color.red;
            //攻击范围线
            Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + attackDistance * facingDir,transform.position.y));

            #endregion

            #region 攻击范围

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);

            #endregion
        }
    }
}