using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.PlayerControl.PlayerState
{
    /// <summary>
    /// Player死亡状态
    /// <remarks>无动画，实现的是马里奥式坠落死亡特效</remarks>>
    /// </summary>
    public class PlayerDead : EntityState
    {
        private Player player;
        public PlayerDead(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
        {
            player = _entity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            //死亡时定格在上一个状态的动画
            player.anim.SetBool(player.lastAnimBoolName,true);
            player.anim.speed = 0;
            //坠落
            player.capsuleCD.enabled = false;
            
            //在后续的OnExit中实现先跳跃再坠落
            stateTimer = .1f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //先跳跃再坠落
            if (stateTimer > 0)
                player.rb.velocity = new Vector2(0, 8);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}