using Managers;
using Signals;
using Zenject;

namespace Commands
{
    public class AddPointsCommand : ICommandWithParameters
    {
        [Inject] private PointsManager pointsManager;
        
        public void Execute(ISignal signal)
        {
            var parameters = (AddPointsSignal) signal;
            pointsManager.AddPoints(parameters.PointsValue);
        }
    }
}