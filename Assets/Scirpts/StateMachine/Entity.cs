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
        //组件
        public Animator anim;
        public Rigidbody2D rb;
        
        //新输入系统
        [HideInInspector] public InputSystem inputSystem;
        [HideInInspector] public Vector2 inputMoveVec2;
        [HideInInspector] public float inputMoveVec2_X;
        [HideInInspector] public float inputMoveVec2_Y;
        
        private bool b_FacingRight = true;

        [Header("碰撞体检测")] 
        public Transform groundCheck;
        public float groundCheckDistance;
        public LayerMask whatIsGround;
        
        
        protected virtual void Awake()
        {
            //新输入系统实例
            inputSystem = new InputSystem();
        }

        protected virtual void Start()
        {
            //组件实例
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
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

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //地面检测线
            Gizmos.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        }
        #endregion
        
        #region Flip

        /// <summary>
        /// 翻转逻辑
        /// </summary>
        public virtual void Flip()
        {
            b_FacingRight = !b_FacingRight;
            
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
        
        private void OnEnable()
        {
            inputSystem.Enable();
        }

        private void OnDisable()
        {
            inputSystem.Disable();
        }
    }
}