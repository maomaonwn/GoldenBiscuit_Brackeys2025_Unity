using UnityEngine;

namespace Scirpts.PlayerControl.PlayerState
{
    public class PlayerWalk : EntityState
    {
        private Player player;
        public PlayerWalk(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
        {
            player = _entity;
        }
    }
}