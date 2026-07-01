using Diamonds;
using Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Diamond diamondPrefab;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<DiamondCollectedSignal>();
            Container.DeclareSignal<DiamondsSpawnedSignal>();
            //Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<AllDiamondsCollectedSignal>();
            //Container.DeclareSignal<GameWinSignal>();
            //Container.DeclareSignal<GameLoseSignal>();

            //Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiamondManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiamondSpawner>().AsSingle();
            //Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            
            Container.BindFactory<Diamond, Factory>()
                .FromComponentInNewPrefab(diamondPrefab)
                .UnderTransformGroup("Diamonds");
        }
    }
}