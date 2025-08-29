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

            player = GameObject.FindWithTag("Player").transform;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //追击朝向
            if(player.position.x>enemy.transform.position.x)    //玩家在普通敌人右边时
                moveDir = 1;
            else if(player.position.x < enemy.transform.position.x)
                moveDir = -1;
            //追击
            enemy.SetVelocity(enemy.moveSpeed * moveDir,enemy.rb.velocity.y);
            
            //攻击距离和攻击
            if(enemy.IsPlayerDetected())  //持续检测到敌人时
            {
                stateTimer = enemy.battleTime;

                //->Attack
                if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
                {
                    if(CanAttack())
                        machine.ChangeState(enemy.attackState);
                }
            }
            else  //仍检测到Player时
            {
                //脱战
                //距离太远 或 到达放弃时间
                if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position)>10)
                    machine.ChangeState(enemy.idleState);
                
                machine.ChangeState(enemy.idleState);
            }
        }
        
        /// <summary>
        /// 判断能否攻击
        /// </summary>
        /// <returns></returns>
        private bool CanAttack()
        {
            //判断是否已经结束冷却
            if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
            {
                enemy.lastTimeAttacked = Time.time;
                return true;
            }
            return false;
        }
    }
}