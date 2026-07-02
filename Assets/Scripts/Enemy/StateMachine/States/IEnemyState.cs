namespace Enemy.StateMachine.States
{
    public interface IEnemyState
    {
        public EnemyStateType StateType { get; }
        public void Enter();
        public EnemyStateType Tick();
        public void Exit();
    }
}