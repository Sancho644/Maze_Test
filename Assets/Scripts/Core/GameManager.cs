using System;
using Core.Signals;
using Player;
using Player.Signals;
using Zenject;

namespace Core
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private GameState CurrentState { get; set; }

        public GameManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            CurrentState = GameState.Playing;

            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
            _signalBus.Subscribe<PlayerReachedExitSignal>(OnPlayerReachedExit);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
            _signalBus.Unsubscribe<PlayerReachedExitSignal>(OnPlayerReachedExit);
        }

        private void OnPlayerDied()
        {
            ChangeState(GameState.Lose);
        }

        private void OnPlayerReachedExit()
        {
            ChangeState(GameState.Win);
        }

        private void ChangeState(GameState state)
        {
            if (CurrentState == state)
                return;

            CurrentState = state;

            _signalBus.Fire(new GameStateChangedSignal(state));
        }
    }
}