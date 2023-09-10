using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals
{
    public class OnNewLevelSignal : ISignal
    {
        public int LevelNumber { get; }

        public OnNewLevelSignal(int value)
        {
            LevelNumber = value;
        }
    }
}
