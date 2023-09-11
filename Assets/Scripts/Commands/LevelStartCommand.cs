using Commands;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.Commands
{
    public class LevelStartCommand : ICommandWithParameters
    {
        [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;
        [Inject] private UIManager uiManager;
        
        public void Execute(ISignal signal)
        {
            var parameters = (LevelStartSignal) signal;
            circleObjectPoolFactory.ChangeCirclesColor();
            uiManager.DisplayLevel(parameters.LevelNumber);
        }
    }
}