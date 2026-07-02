using Player;
using Player.Signals;
using UnityEngine;
using Zenject;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyAttack : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            _signalBus.Fire<PlayerDiedSignal>();
        }
    }
}