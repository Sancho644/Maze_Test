using Diamonds.Signals;
using UnityEngine;
using Zenject;

namespace Diamonds
{
    public class Diamond : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(PlayerTag))
                return;

            _signalBus.Fire(new DiamondCollectedSignal(this));

            Destroy(gameObject);
        }
    }
}