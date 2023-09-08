using UnityEngine;

namespace Signals
{
    public class OnPlayParticlesSignal : ISignal
    {
        public Color ParticleColor { get; }
        public Vector2 ParticlePosition { get; }

        public OnPlayParticlesSignal(Color colorValue, Vector2 positionValue)
        {
            ParticleColor = colorValue;
            ParticlePosition = positionValue;
        }
    }
}