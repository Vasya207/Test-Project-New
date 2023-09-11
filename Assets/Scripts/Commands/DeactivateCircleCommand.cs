using Signals;
using Zenject;

namespace Commands
{
    public class DeactivateCircleCommand : ICommandWithParameters
    {
        [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;
        
        public void Execute(ISignal signal)
        {
            var parameters = (DeactivateCircleSignal) signal;
            circleObjectPoolFactory.DeactivateCircle(parameters.CircleObj);
        }
    }
}