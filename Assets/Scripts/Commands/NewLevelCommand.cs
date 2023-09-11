using CircleComponents;
using Managers;
using Signals;
using Zenject;

namespace Commands
{
    public class NewLevelCommand : ICommandWithParameters
    {
        [Inject] private UIManager uiManager;
        [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;

        public void Execute(ISignal signal)
        {
            var parameters = (NewLevelSignal) signal;
            uiManager.DisplayLevel(parameters.LevelNumber);
            circleObjectPoolFactory.IncreaseDifficulty();
            circleObjectPoolFactory.ChangeCirclesColor();
        }
    }
}
