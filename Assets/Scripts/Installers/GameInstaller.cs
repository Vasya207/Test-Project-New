using System;
using Core.Commands;
using Signals;
using Commands;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] private GameSettingsInstaller.CirclePrefabSettings circlePrefabSettings;
    public override void InstallBindings()
    {
        InstallComponents();
        InstallFactory();
        InstallSignals();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<NewLevelSignal>();
        Container.BindSignal<NewLevelSignal>().ToMethod<NewLevelCommand>(command => command.Execute).FromNew();
        
        Container.DeclareSignal<LevelStartSignal>();
        Container.BindSignal<LevelStartSignal>().ToMethod<LevelStartCommand>(command => command.Execute).FromNew();

        Container.DeclareSignal<AddPointsSignal>();
        Container.BindSignal<AddPointsSignal>().ToMethod<AddPointsCommand>(command => command.Execute).FromNew();

        Container.DeclareSignal<DeactivateCircleSignal>();
        Container.BindSignal<DeactivateCircleSignal>().ToMethod<DeactivateCircleCommand>(command => command.Execute).FromNew();

        Container.DeclareSignal<PlayParticlesSignal>();
        Container.BindSignal<PlayParticlesSignal>().ToMethod<PlayParticlesCommand>(command => command.Execute).FromNew();
    }

    private void InstallComponents()
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
        Container.BindFactory<Circle, Circle.Factory>().FromComponentInNewPrefab(circlePrefabSettings.CirclePrefab);
    }
}