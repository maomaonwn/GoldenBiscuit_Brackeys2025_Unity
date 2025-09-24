namespace Scirpts.StateMachine
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
            if (b_AllStatesDisabled) return;
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
            //冻结状态
            // currentState = 某冻结状态
            
            b_AllStatesDisabled = true;
        }

        /// <summary>
        /// 重新启动状态机
        /// </summary>
        /// <param name="_startState"></param>
        public void EnableStateMachine(EntityState _startState)
        {
            //恢复禁用标志
            b_AllStatesDisabled = false;
            //切换状态
            ChangeState(_startState);
        }
    }
}