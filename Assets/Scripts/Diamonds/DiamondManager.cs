using System;
using Core;
using Diamonds.Signals;
using Zenject;

namespace Diamonds
{
    public class DiamondManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private int _remainingDiamonds;
        private int _totalDiamonds;
        
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
            _totalDiamonds = signal.Count;
            _remainingDiamonds = signal.Count;

            PublishCounter();
        }

        private void OnDiamondCollected(DiamondCollectedSignal signal)
        {
            _remainingDiamonds--;

            PublishCounter();

            if (_remainingDiamonds <= 0)
            {
                _signalBus.Fire<AllDiamondsCollectedSignal>();
            }
        }
        
        private void PublishCounter()
        {
            _signalBus.Fire(new DiamondsCountChangedSignal(
                new CollectibleProgress(
                    _totalDiamonds - _remainingDiamonds,
                _totalDiamonds)));
        }
    }
}