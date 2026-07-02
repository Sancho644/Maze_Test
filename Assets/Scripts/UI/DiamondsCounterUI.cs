using Diamonds.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class DiamondsCounterUI : MonoBehaviour
    {
        [SerializeField] private Text counterText;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<DiamondsCountChangedSignal>(UpdateCounter);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<DiamondsCountChangedSignal>(UpdateCounter);
        }

        private void UpdateCounter(DiamondsCountChangedSignal signal)
        {
            counterText.text = $"{signal.Progress.Collected}/{signal.Progress.Total}";
        }
    }
}