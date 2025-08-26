using UnityEngine;

namespace Scirpts.PlayerControl
{
    /// <summary>
    /// 玩家动画事件方法
    /// </summary>
    public class PlayerAnimationEvent : MonoBehaviour
    {
        private Player player => GetComponentInParent <Player>();

        /// <summary>
        /// 通用动画触发函数
        /// </summary>
        private void AnimationTrigger() => player.AnimationTrigger();
    }
}