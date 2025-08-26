using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl
{
    public class Enemy : Entity
    {
        //状态

        protected override void Awake()
        {
            base.Awake();
            //状态实例
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