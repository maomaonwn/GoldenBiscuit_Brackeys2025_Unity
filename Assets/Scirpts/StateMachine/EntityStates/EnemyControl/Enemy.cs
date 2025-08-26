using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl
{
    public class Enemy : Entity
    {
        //状态机
        public StateMachine machine {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            //状态机实例
            machine = new StateMachine();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            //状态的帧执行
            machine.currentState.OnUpdate();
        }
    }
}