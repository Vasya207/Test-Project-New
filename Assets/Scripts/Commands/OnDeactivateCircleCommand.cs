using Signals;
using Zenject;

namespace Commands
{
    public class OnDeactivateCircleCommand : ICommandWithParameters
    {
        [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;
        
        public void Execute(ISignal signal)
        {
            var parameters = (OnDeactivateCircleSignal) signal;
            circleObjectPoolFactory.DeactivateCircle(parameters.CircleObj);
        }
    }
}