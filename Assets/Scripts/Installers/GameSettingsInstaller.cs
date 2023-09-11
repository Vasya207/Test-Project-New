using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public CirclePrefabSettings Circle;
    public ParticleSettings Particles;
    public CircleObjectPoolFactorySettings CircleOPFactory;
    public LevelManagerSettings LevelManager;
    
    [Serializable]
    public class ParticleSettings
    {
        public ParticleSystemManager.Settings ParticleSystemParameters;
    }
    
    [Serializable]
    public class CircleObjectPoolFactorySettings
    {
        public CircleObjectPoolFactory.Settings CircleParameters;
    }
    
    [Serializable]
    public class LevelManagerSettings
    {
        public LevelManager.Settings LevelManagerParameters;
    }    
    
    [Serializable]
    public class CirclePrefabSettings
    {
        public Circle CirclePrefab;
    }
    
    public override void InstallBindings()
    {
        Container.BindInstance(Circle);
        Container.BindInstance(Particles.ParticleSystemParameters);
        Container.BindInstance(CircleOPFactory.CircleParameters);
        Container.BindInstance(LevelManager.LevelManagerParameters);
    }
}