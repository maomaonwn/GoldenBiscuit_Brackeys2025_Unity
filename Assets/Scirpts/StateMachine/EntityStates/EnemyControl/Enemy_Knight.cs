using Scirpts.EntityStat;
using Scirpts.EntityStates.EnemyControl.EnemyKnightState;
using Unity.VisualScripting;
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
        public KnightDead deadState { get; private set; }
        
        public float idleTime;
        
        [Header("玩家检测")]
        public Transform playerCheck;
        public LayerMask whatIsPlayer;
        [Tooltip("追击持续时间")]
        public float battleTime = 7f;    //追击持续时间
        [Header("攻击")]
        [Tooltip("最远攻击距离")]
        public float attackDistance = 1.5f;    //最远攻击距离
        public float attackCooldown = .6f;
        [Header("晕眩")] 
        protected bool b_BeStunned;
        public float stunnedDuration;
        public Vector2 stunnedPower;
        [SerializeField]protected GameObject counterImage;
        
        //EnemyStat
        public KnightStat enemyStat { get;private set; }

        protected override void Awake()
        {
            base.Awake();
            //状态实例
            idleState = new KnightIdle(this, machine, "Idle", this);
            moveState = new KnightMove(this, machine, "Move", this);
            battleState = new KnightBattle(this, machine, "Move", this);
            attackState = new KnightAttack(this, machine, "Attack", this);
            stunnedState = new KnightStunned(this, machine, "Stunned", this);
            deadState = new KnightDead(this, machine, "Dead", this);

            enemyStat = GetComponent<KnightStat>();
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

        #region CounterAttack

        // public virtual bool EnterStunned()
        // {
        //     if (CanBeStunned())
        //     {
        //         machine.ChangeState(stunnedState);
        //         return true;
        //     }
        //
        //     return false;
        // }
        
        /// <summary>
        /// 晕眩(->Stunned)
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBeStunned()
        {
            if(b_BeStunned)
            {
                CloseCounterAttackWindow();
                return true;
            }
            return false;
        }
        
        public virtual void OpenCounterAttackWindow()
        {
            b_BeStunned = true;
            counterImage.SetActive(true);
        }
        public virtual void CloseCounterAttackWindow()
        {
            b_BeStunned = false;
            counterImage.SetActive(false);
        }

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
        }
    }
}