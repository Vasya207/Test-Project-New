using Signals;
using Zenject;

namespace Commands
{
    public class OnAddPointsCommand : ICommandWithParameters
    {
        [Inject] private PointsManager pointsManager;
        
        public void Execute(ISignal signal)
        {
            var parameters = (OnAddPointsSignal) signal;
            pointsManager.AddPoints(parameters.PointsValue);
        }
    }
}