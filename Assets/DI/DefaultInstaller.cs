using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private Circle circle;
    public override void InstallBindings()
    {
        Container.Bind<ParticleSystemController>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PointsManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CircleCleaner>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BoundariesInitializer>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CircleFactory>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindFactory<Circle, Circle.Factory>().FromComponentInNewPrefab(circle);
    }
}