namespace Enemy.StateMachine.States
{
    public class ChaseState : IEnemyState
    {
        public EnemyStateType StateType => EnemyStateType.Chase;
        
        private readonly EnemyMovement _movement;
        private readonly EnemyVision _vision;

        public ChaseState(EnemyMovement movement, EnemyVision vision)
        {
            _movement = movement;
            _vision = vision;
        }

        public void Enter()
        {
        }

        public EnemyStateType  Tick()
        {
            if (!_vision.CanSeePlayer())
            {
                return EnemyStateType.Patrol;
            }

            _movement.MoveTo(_vision.Player.position);
            
            return StateType;
        }

        public void Exit()
        {
        }
    }
}