using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Scirpts
{
    /// <summary>
    /// Entity实例脚本
    /// </summary>
    public class Entity : MonoBehaviour
    {
        //状态机
        public StateMachine machine;
        
        //组件
        public Animator anim;
        public Rigidbody2D rb;
        public EntityFX fx;
        public CapsuleCollider2D capsuleCD { get; private set; }   //碰撞体
        
        //新输入系统
        [HideInInspector] public InputSystem inputSystem;
        [HideInInspector] public Vector2 inputMoveVec2;
        [HideInInspector] public float inputMoveVec2_X;
        [HideInInspector] public float inputMoveVec2_Y;
        
        private bool b_FacingRight = true;
        public int facingDir { get; private set; } = 1;
        
        [Header("地面检测")] 
        public Transform groundCheck;
        public float groundCheckDistance;
        public LayerMask whatIsGround;
        [Header("攻击检测")]
        public Transform attackCheck;
        public float attackCheckRadius;
        
        [Header("移动")] 
        public float moveSpeed = 1f;
        
        [HideInInspector]public float lastTimeAttacked;
        
        public string lastAnimBoolName { get; private set; }
        
        protected virtual void Awake()
        {
            //状态机实例
            machine = new StateMachine();
            //新输入系统实例
            inputSystem = new InputSystem();
 
        }

        protected virtual void Start()
        {
            //组件实例
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            fx = GetComponent<EntityFX>();
            capsuleCD = GetComponent<CapsuleCollider2D>();
            //状态实例在下级实例脚本中
        }

        protected virtual void Update()
        {
            //读取移动输入
            inputMoveVec2 = inputSystem.Gameplay.Move.ReadValue<Vector2>();
            inputMoveVec2_X = inputMoveVec2.x;
            inputMoveVec2_Y = inputMoveVec2.y;
        }

        #region CollisionCheck

        /// <summary>
        /// 地面检测
        /// </summary>
        /// <returns></returns>
        public virtual bool IsGroundDetected()=>
            Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance, whatIsGround);
        
        #endregion
        
        #region Flip

        /// <summary>
        /// 翻转逻辑
        /// </summary>
        public virtual void Flip()
        {
            b_FacingRight = !b_FacingRight;
            facingDir *= -1;
            
            transform.Rotate(0,180,0);
        }

        /// <summary>
        /// 翻转控制
        /// </summary>
        /// <param name="_x"></param>
        public virtual void FlipController(float _x)
        {
            if(_x > 0 && !b_FacingRight)
                Flip();
            else if (_x < 0 && b_FacingRight)
                Flip();
        }

        #endregion
        
        #region Velocity
        
        /// <summary>
        /// 设置速度
        /// </summary>
        /// <param name="_xVelocity"></param>
        /// <param name="_yVelocity"></param>
        public virtual void SetVelocity(float _xVelocity, float _yVelocity)
        {
            rb.velocity = new Vector2(_xVelocity, _yVelocity);
            FlipController(_xVelocity);
        }

        /// <summary>
        /// 设置速度为0
        /// </summary>
        public virtual void SetZeroVelocity()
        {
            rb.velocity = new Vector2(0, 0);
        }
        
        #endregion

        #region Animation

        /// <summary>
        /// 通用动画触发函数
        /// </summary>
        public void AnimationTrigger() => machine.currentState.AnimationTrigger();

        /// <summary>
        /// 获取最后状态的动画名
        /// </summary>
        /// <param name="_animBoolName"></param>
        public virtual void AssignLastAnimName(string _animBoolName)
        {
            lastAnimBoolName = _animBoolName;
        }
        
        #endregion

        #region Hit and Dead

        /// <summary>
        /// 受伤
        /// </summary>
        public virtual void Damage()
        {
            //受伤时的闪烁特效
            fx.StartCoroutine("FlashFX");
        }

        /// <summary>
        /// 死亡
        /// <remarks>此函数是写在状态机框架中的函数，用于实现全局死亡检测，随时切换到死亡状态中</remarks>
        /// </summary>
        public virtual void Die()
        {
            
        }

        #endregion
        
        #region Destroy

        /// <summary>
        /// 销毁物体
        /// </summary>
        public virtual void SelfDestroy() => Destroy(gameObject);

        #endregion
        
        private void OnEnable()
        {
            inputSystem.Enable();
        }

        private void OnDisable()
        {
            inputSystem.Disable();
        }
        
        /// <summary>
        /// 可视化Debug
        /// </summary>
        protected virtual void OnDrawGizmos()
        {
            #region 地面检测

            Gizmos.color = Color.red;
            //地面检测线
            Gizmos.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));

            #endregion
            
            #region 攻击范围

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);

            #endregion
        }
    }
}