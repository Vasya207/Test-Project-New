using Signals;
using Zenject;

namespace Commands
{
    public class OnPlayParticlesCommand : ICommandWithParameters
    {
        [Inject] private ParticleSystemManager particleSystemManager;
        
        public void Execute(ISignal signal)
        {
            var parameters = (OnPlayParticlesSignal) signal;
            particleSystemManager.PlayParticles(parameters.ParticleColor, parameters.ParticlePosition);
        }
    }
}