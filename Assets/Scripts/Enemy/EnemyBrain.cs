using System;
using System.Collections.Generic;
using Core;
using Core.Signals;
using Enemy.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyVision enemyVision;
        [SerializeField] private PatrolPath patrolPath;

        private Dictionary<EnemyStateType, IEnemyState> _states;

        private IEnemyState _currentState;
        private SignalBus _signalBus;

        private bool _enabled = true;
        
        private void Awake()
        {
            _states = new Dictionary<EnemyStateType, IEnemyState>
            {
                { EnemyStateType.Patrol, new PatrolState(enemyMovement, enemyVision, patrolPath) },
                { EnemyStateType.Chase, new ChaseState(enemyMovement, enemyVision) }
            };
            
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void Start()
        {
            ChangeState(EnemyStateType.Patrol);
        }

        private void Update()
        {
            if (!_enabled)
                return;
            
            EnemyStateType nextState = _currentState.Tick();

            if (nextState != _currentState.StateType)
            {
                ChangeState(nextState);
            }
        }
        
        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void ChangeState(EnemyStateType stateType)
        {
            _currentState?.Exit();

            _currentState = _states[stateType];

            _currentState.Enter();
        }
        
        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.State != GameState.Playing)
                _enabled = false;
        }
    }
}