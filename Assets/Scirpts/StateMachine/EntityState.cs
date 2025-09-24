using UnityEngine;

namespace Scirpts.StateMachine
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
        //当前动画状态信息
        protected AnimatorStateInfo animStateInfo;

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
            //获取当前动画状态信息
            animStateInfo = entity.anim.GetCurrentAnimatorStateInfo(0);
            
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
            entity.AssignLastAnimName(animBoolName);
        }

        /// <summary>
        /// 通用动画触发函数
        /// </summary>
        public void AnimationTrigger()
        {
            b_TriggerCalled = true;
        }

        /// <summary>
        /// 销毁物体
        /// </summary>
        public virtual void SelfDestroy() => entity.SelfDestroy();
        
    }
}