using Core;
using UnityEngine;
using Zenject;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    private ParticleSystem.MainModule particleSettings;
    
    [Inject] private void Construct()
    {
        particleSettings = particleSystem.main;
    }

    public void PlayParticles(SpriteRenderer spriteRenderer, Vector3 position)
    {
        particleSystem.transform.position = position;
        particleSettings.startColor = new ParticleSystem.MinMaxGradient(spriteRenderer.color);
        particleSystem.Play();
    }
}
