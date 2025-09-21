using UnityEngine;

namespace Scirpts
{
    /// <summary>
    /// 状态机
    /// </summary>
    public class StateMachine
    {
        public EntityState currentState { get; private set; }
        
        public bool b_AllStatesDisabled { get; private set; }

        public void Initialize(EntityState _startState)
        {
            currentState = _startState;
            currentState.OnEnter();
        }

        public void ChangeState(EntityState _newState)
        {
            currentState.OnExit();
            currentState = _newState;
            currentState.OnEnter();
        }

        /// <summary>
        /// 禁用所有状态
        /// </summary>
        public void DisableAllStates()
        {
            //退出当前状态
            currentState?.OnExit();
            //清空状态
            currentState = null;
            
            b_AllStatesDisabled = true;
        }

        /// <summary>
        /// 重新启动状态机
        /// </summary>
        /// <param name="_startState"></param>
        public void EnableStateMachine(EntityState _startState)
        {
            ChangeState(_startState);

            b_AllStatesDisabled = false;
        }
    }
}