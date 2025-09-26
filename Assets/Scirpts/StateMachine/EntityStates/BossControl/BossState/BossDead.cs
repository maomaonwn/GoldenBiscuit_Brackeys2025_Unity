using Unity.VisualScripting;

namespace Scirpts.StateMachine.EntityStates.BossControl.BossState
{
    public class BossDead : EntityState
    {
        private Boss boss;
        public BossDead(Entity _entityBase, StateMachine _machine, string _animBoolName,Boss _entity) : base(_entityBase, _machine, _animBoolName)
        {
            boss = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            boss.b_IsEntityDead = true;
        }
    }
}