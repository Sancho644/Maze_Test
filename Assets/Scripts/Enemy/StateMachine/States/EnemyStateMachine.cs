namespace Enemy.StateMachine.States
{
    public class EnemyStateMachine
    {
        public IEnemyState CurrentState { get; private set; }

        public void ChangeState(IEnemyState newState)
        {
            if (CurrentState == newState)
                return;

            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();
        }

        public void Tick()
        {
            CurrentState?.Tick();
        }
    }
}