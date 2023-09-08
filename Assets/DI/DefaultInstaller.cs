using Core.Commands;
using Signals;
using Commands;
using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private Circle circle;
    public override void InstallBindings()
    {
        InstallObjects();
        InstallFactory();
        InstallSignals();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<OnNewLevelSignal>();
        Container.BindSignal<OnNewLevelSignal>().ToMethod<OnNewLevelCommand>(signal => signal.Execute).FromNew();
        
        Container.DeclareSignal<OnLevelStartSignal>();
        Container.BindSignal<OnLevelStartSignal>().ToMethod<OnLevelStartCommand>(signal => signal.Execute).FromNew();
    }

    private void InstallObjects()
    {
        Container.Bind<ParticleSystemController>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PointsManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CircleCleaner>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BoundariesInitializer>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CircleObjectPoolFactory>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }

    private void InstallFactory()
    {
        Container.BindFactory<Circle, Circle.Factory>().FromComponentInNewPrefab(circle);
    }
}