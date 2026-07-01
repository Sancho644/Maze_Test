using System;
using Signals;
using Zenject;

namespace Diamonds
{
    public class DiamondManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private int _remainingDiamonds;

        public DiamondManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<DiamondsSpawnedSignal>(OnDiamondsSpawned);
            _signalBus.Subscribe<DiamondCollectedSignal>(OnDiamondCollected);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DiamondsSpawnedSignal>(OnDiamondsSpawned);
            _signalBus.Unsubscribe<DiamondCollectedSignal>(OnDiamondCollected);
        }

        private void OnDiamondsSpawned(DiamondsSpawnedSignal signal)
        {
            _remainingDiamonds = signal.Count;
        }

        private void OnDiamondCollected(DiamondCollectedSignal signal)
        {
            _remainingDiamonds--;

            if (_remainingDiamonds <= 0)
            {
                _signalBus.Fire<AllDiamondsCollectedSignal>();
            }
        }
    }
}