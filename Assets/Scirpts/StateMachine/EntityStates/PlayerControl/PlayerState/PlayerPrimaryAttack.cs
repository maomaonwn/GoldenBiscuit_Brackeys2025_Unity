using Scirpts.Manager;
using UnityEngine;

namespace Scirpts.StateMachine.EntityStates.PlayerControl.PlayerState
{
    public class PlayerPrimaryAttack : EntityState
    {
        private Player player;
        public PlayerPrimaryAttack(Entity _entityBase, StateMachine _machine, string _animBoolName,Player _entity) : base(_entityBase, _machine, _animBoolName)
        {
            player = _entity;
        }

        private int comboCounter;

        public override void OnEnter()
        {
            base.OnEnter();

            //计时器（用于制作攻击动作的惯性）
            stateTimer = .1f;
            
            //攻击朝向
            float attackDir = player.facingDir;
            if (player.inputMoveVec2_X != 0)
                attackDir = player.inputMoveVec2_X;
            //攻击时产生的位移
            player.SetVelocity(player.attackMovement[comboCounter].x * attackDir ,player.attackMovement[comboCounter].y);
            
            //攻击音效
            AudioManager.instance.PlaySoundEffect(SoundEffectName.Player_Attack);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            //移动时攻击停下，并有惯性
            if(stateTimer < 0)
                player.SetZeroVelocity();
            
            //->Idle
            //动画事件触发（攻击结束）
            if(b_TriggerCalled)
                machine.ChangeState(player.idleState);
        }

        public override void OnExit()
        {
            base.OnExit();

            player.lastTimeAttacked = Time.time;

            player.StartCoroutine("BusyFor", .1f);
        }
    }
}