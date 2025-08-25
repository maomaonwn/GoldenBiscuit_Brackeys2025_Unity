using UnityEngine;

namespace Scirpts
{
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
        }

        protected virtual void Update()
        {
            
        }
    }
}