using Scirpts.StateMachine.EntityStates.EnemyControl;

namespace Scirpts.StateMachine.EntityStat
{
    public class KnightStat : EntityStat
    {
        private Enemy_Knight enemy => GetComponent<Enemy_Knight>();

        public override void Die()
        {
            base.Die();

            //->Dead 切换到死亡状态 
            enemy.Die();
        }
    }
}