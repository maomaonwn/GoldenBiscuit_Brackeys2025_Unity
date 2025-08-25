using System;
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
        
        protected virtual void Awake()
        {
            
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