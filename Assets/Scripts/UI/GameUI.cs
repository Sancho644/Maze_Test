using Core;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private SignalBus _signalBus;

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
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

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            switch (signal.State)
            {
                case GameState.Win:
                    winPanel.SetActive(true);
                    break;

                case GameState.Lose:
                    losePanel.SetActive(true);
                    break;
            }
        }
    }
}