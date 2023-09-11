using UnityEngine;

namespace Signals
{
    public class PlayParticlesSignal : ISignal
    {
        public Color ParticleColor { get; }
        public Vector2 ParticlePosition { get; }

        public PlayParticlesSignal(Color colorValue, Vector2 positionValue)
        {
            ParticleColor = colorValue;
            ParticlePosition = positionValue;
        }
    }
}