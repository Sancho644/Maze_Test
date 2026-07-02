using Core;
using Core.Signals;
using Diamonds;
using Diamonds.Signals;
using Player.Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        private const string DiamondsTransformGroupName = "Diamonds";
        
        [SerializeField] private Diamond diamondPrefab;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            InstallSignals();

            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiamondManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiamondSpawner>().AsSingle();

            Container.BindFactory<Diamond, Factory>()
                .FromComponentInNewPrefab(diamondPrefab)
                .UnderTransformGroup(DiamondsTransformGroupName);
            
            Container.Bind<Player.Player>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<DiamondCollectedSignal>();
            Container.DeclareSignal<DiamondsSpawnedSignal>();
            Container.DeclareSignal<DiamondsCountChangedSignal>();
            Container.DeclareSignal<AllDiamondsCollectedSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<PlayerReachedExitSignal>();
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<GameWinSignal>();
            Container.DeclareSignal<GameLoseSignal>();
        }
    }
}