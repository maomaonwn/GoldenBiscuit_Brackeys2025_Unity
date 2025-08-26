using UnityEngine;

namespace Scirpts
{
    /// <summary>
    /// Entity状态基类
    /// </summary>
    public class EntityState
    {
        //状态机相关
        protected StateMachine machine;
        protected Entity entity;
        protected string animBoolName;
        
        //通用计时器
        protected float stateTimer;
        //通用动画事件触发器
        protected bool b_TriggerCalled = false;

        public EntityState(Entity _entityBase, StateMachine _machine, string _animBoolName)
        {
            this.entity = _entityBase;
            this.machine = _machine;
            this.animBoolName = _animBoolName;
        }
        
        public virtual void OnEnter()
        {
            //播放动画
            entity.anim.SetBool(animBoolName,true);
            
            //ALWAYS FALSE
            b_TriggerCalled = false;
        }

        public virtual void OnUpdate()
        {
            stateTimer -= Time.deltaTime;
        }

        public virtual void OnExit()
        {
            //停止播放动画
            entity.anim.SetBool(animBoolName,false);
        }

        /// <summary>
        /// 通用动画触发函数
        /// </summary>
        public void AnimationTrigger()
        {
            b_TriggerCalled = true;
        }
    }
}