using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    public class KnightGrounded : EntityState
    {
        private Enemy_Knight enemy;
        public KnightGrounded(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }

        protected Transform player;

        public override void OnEnter()
        {
            base.OnEnter();

            player = GameObject.FindWithTag("Player").transform;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //->Battle
            //检测到玩家 或者 离得太近时
            if(enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position ,player.position)<2)
                machine.ChangeState(enemy.battleState);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}