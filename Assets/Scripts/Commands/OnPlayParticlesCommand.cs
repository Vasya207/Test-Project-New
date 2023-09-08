using Signals;
using Zenject;

namespace Commands
{
    public class OnPlayParticlesCommand : ICommandWithParameters
    {
        [Inject] private ParticleSystemController particleSystemController;
        
        public void Execute(ISignal signal)
        {
            var parameters = (OnPlayParticlesSignal) signal;
            particleSystemController.PlayParticles(parameters.ParticleColor, parameters.ParticlePosition);
        }
    }
}