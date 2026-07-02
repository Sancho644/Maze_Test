using System.Collections.Generic;
using Diamonds.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Diamonds
{
    public class DiamondSpawner : MonoBehaviour
    {
        [SerializeField] private int diamondsToSpawn = 5;
        [SerializeField] private List<Transform> diamondSpawnPoints;

        private Factory _factory;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(Factory factory, SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
        }

        private void Start()
        {
            SpawnDiamonds();
        }

        private void SpawnDiamonds()
        {
            var available = new List<Transform>(diamondSpawnPoints);

            diamondsToSpawn = Mathf.Min(diamondsToSpawn, available.Count);

            for (var i = 0; i < diamondsToSpawn; i++)
            {
                var index = Random.Range(0, available.Count);
                var point = available[index];

                Diamond diamond = _factory.Create();

                diamond.transform.SetPositionAndRotation(
                    point.position,
                    Quaternion.identity);

                available.RemoveAt(index);
            }

            _signalBus.Fire(new DiamondsSpawnedSignal(diamondsToSpawn));
        }
    }
}