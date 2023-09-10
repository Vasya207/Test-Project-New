using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ParticleSystemManager : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule particleSettings;
    
    [Inject] private void Construct(Settings set)
    {
        particleSystem = Instantiate(set.ParticlePrefab);
        particleSystem.transform.SetParent(transform);
        particleSettings = particleSystem.main;
    }

    public void PlayParticles(Color color, Vector3 position)
    {
        particleSystem.transform.position = position;
        particleSettings.startColor = new ParticleSystem.MinMaxGradient(color);
        particleSystem.Play();
    }
    
    [Serializable]
    public class Settings
    {
        public ParticleSystem ParticlePrefab;
    }
}
