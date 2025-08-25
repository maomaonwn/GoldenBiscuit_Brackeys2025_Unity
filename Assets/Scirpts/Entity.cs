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
            
        }
    }
}