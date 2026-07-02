using Common;
using Diamonds.Signals;
using Player.Signals;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ExitController : MonoBehaviour
    {
        private SignalBus _signalBus;

        private bool _isUnlocked;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Awake()
        {
            _signalBus.Subscribe<AllDiamondsCollectedSignal>(UnlockExit);
        }

        public void OnDestroy()
        {
            _signalBus.Unsubscribe<AllDiamondsCollectedSignal>(UnlockExit);
        }

        private void UnlockExit()
        {
            _isUnlocked = true;

            Debug.Log("Exit unlocked");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isUnlocked)
                return;

            if (!other.CompareTag(GameConstants.Tags.Player))
                return;

            _signalBus.Fire<PlayerReachedExitSignal>();
        }
    }
}