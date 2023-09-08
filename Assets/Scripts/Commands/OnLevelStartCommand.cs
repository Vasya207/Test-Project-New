using Signals;
using UnityEngine;
using Zenject;

namespace Core.Commands
{
    public class OnLevelStartCommand : ICommand
    {
        [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;
        
        public void Execute()
        {
            circleObjectPoolFactory.ChangeCirclesColor();
        }
    }
}