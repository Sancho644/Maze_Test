namespace Enemy.StateMachine.States
{
    public class PatrolState : IEnemyState
    {
        public EnemyStateType StateType => EnemyStateType.Patrol;
        
        private readonly EnemyMovement _movement;
        private readonly EnemyVision _vision;
        private readonly PatrolPath _path;

        private int _currentPoint;

        public PatrolState(EnemyMovement movement, EnemyVision vision, PatrolPath path)
        {
            _movement = movement;
            _vision = vision;
            _path = path;
        }

        public void Enter()
        {
            MoveNext();
        }

        public EnemyStateType Tick()
        {
            if (_vision.CanSeePlayer())
            {
                return EnemyStateType.Chase;
            }

            if (_movement.ReachedDestination)
            {
                MoveNext();
            }

            return StateType;
        }

        public void Exit()
        {
        }

        private void MoveNext()
        {
            if (_path == null)
            {
                return;
            }
            
            _movement.MoveTo(_path.GetPoint(_currentPoint).position);

            _currentPoint = (_currentPoint + 1) % _path.Count;
        }
    }
}