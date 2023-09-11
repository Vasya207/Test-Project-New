using Managers;
using Signals;
using Zenject;

namespace Commands
{
    public class PlayParticlesCommand : ICommandWithParameters
    {
        [Inject] private ParticleSystemManager particleSystemManager;
        
        public void Execute(ISignal signal)
        {
            var parameters = (PlayParticlesSignal) signal;
            particleSystemManager.PlayParticles(parameters.ParticleColor, parameters.ParticlePosition);
        }
    }
}