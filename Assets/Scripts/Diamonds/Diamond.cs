using Common;
using Diamonds.Signals;
using UnityEngine;
using Zenject;

namespace Diamonds
{
    public class Diamond : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameConstants.Tags.Player))
                return;

            _signalBus.Fire(new DiamondCollectedSignal(this));

            Destroy(gameObject);
        }
    }
}