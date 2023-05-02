using Core;
using UnityEngine;

public class ParticleSystemController : Singleton<ParticleSystemController>
{
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule particlesSettings;
    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particlesSettings = GetComponentInChildren<ParticleSystem>().main;
    }

    public void PlayParticles(SpriteRenderer spriteRenderer, Vector3 position)
    {
        particleSystem.transform.position = position;
        particlesSettings.startColor = new ParticleSystem.MinMaxGradient(spriteRenderer.color);
        particleSystem.Play();
    }
}
