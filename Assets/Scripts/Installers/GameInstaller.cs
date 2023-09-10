using System;
using Core.Commands;
using Signals;
using Commands;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] private Settings settings;
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

        Container.DeclareSignal<OnAddPointsSignal>();
        Container.BindSignal<OnAddPointsSignal>().ToMethod<OnAddPointsCommand>(signal => signal.Execute).FromNew();

        Container.DeclareSignal<OnDeactivateCircleSignal>();
        Container.BindSignal<OnDeactivateCircleSignal>().ToMethod<OnDeactivateCircleCommand>(signal => signal.Execute).FromNew();

        Container.DeclareSignal<OnPlayParticlesSignal>();
        Container.BindSignal<OnPlayParticlesSignal>().ToMethod<OnPlayParticlesCommand>(signal => signal.Execute).FromNew();
    }

    private void InstallObjects()
    {
        Container.Bind<PointsManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<CircleCleaner>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BoundariesInitializerManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<CircleObjectPoolFactory>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ParticleSystemManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }

    private void InstallFactory()
    {
        Container.BindFactory<Circle, Circle.Factory>().FromComponentInNewPrefab(settings.CirclePrefab);
    }
    
    [Serializable]
    public class Settings
    {
        public Circle CirclePrefab;
    }
}