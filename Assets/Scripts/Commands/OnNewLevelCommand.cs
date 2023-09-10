using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class OnNewLevelCommand : ICommandWithParameters
    {
        [Inject] private UIManager uiManager;
        [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;

        public void Execute(ISignal signal)
        {
            var parameters = (OnNewLevelSignal) signal;
            uiManager.DisplayLevel(parameters.LevelNumber);
            circleObjectPoolFactory.IncreaseDifficulty();
            circleObjectPoolFactory.ChangeCirclesColor();
        }
    }
}
