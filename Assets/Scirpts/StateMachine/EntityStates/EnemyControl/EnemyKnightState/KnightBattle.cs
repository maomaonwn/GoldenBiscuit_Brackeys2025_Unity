using UnityEngine;

namespace Scirpts.EntityStates.EnemyControl.EnemyKnightState
{
    /// <summary>
    /// 交战逻辑（过渡状态）
    /// </summary>
    public class KnightBattle : EntityState
    {
        private Enemy_Knight enemy;
        public KnightBattle(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }

        private Transform player;
        private int moveDir;

        public override void OnEnter()
        {
            base.OnEnter();

            player = GameObject.Find("Player").transform;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //追击朝向
            if (player.position.x > enemy.transform.position.x)     //玩家在普通敌人右边时
                moveDir = 1;
            else if (player.position.x < enemy.transform.position.x)    //在左边时
                moveDir = -1;
            //追击
            enemy.SetVelocity(enemy.moveSpeed * moveDir,enemy.rb.velocity.y);

            //确保仍能看见Player
            if (enemy.IsPlayerDetected())
            {
                //追击持续时间
                stateTimer = enemy.battleTime;
                
                //进入攻击范围时
                if (enemy.IsPlayerDetected().distance < enemy.attckDistance)
                {
                    //->Attack
                    if(CanAttack())
                        machine.ChangeState(enemy.attackState);
                }
            }
            else
            {
                //超时 || 距离太远，脱战
                if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position)>10)
                    machine.ChangeState(enemy.idleState);
                
                //看不见Player了，脱战
                machine.ChangeState(enemy.idleState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
        
        /// <summary>
        /// 判断是否能攻击
        /// </summary>
        /// <returns></returns>
        private bool CanAttack()
        {
            //判断是否已经冷却完毕
            if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
            {
                enemy.lastTimeAttacked = Time.time;
                return true;
            }
            return false;
        }
    }
}