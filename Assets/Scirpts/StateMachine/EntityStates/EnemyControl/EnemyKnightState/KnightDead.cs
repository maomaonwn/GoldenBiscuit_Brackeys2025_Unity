namespace Scirpts.StateMachine.EntityStates.EnemyControl.EnemyKnightState
{
    public class KnightDead : EntityState
    {
        private Enemy_Knight enemy;
        public KnightDead(Entity _entityBase, StateMachine _machine, string _animBoolName,Enemy_Knight _entity) : base(_entityBase, _machine, _animBoolName)
        {
            enemy = _entity;
        }
        
    }
}