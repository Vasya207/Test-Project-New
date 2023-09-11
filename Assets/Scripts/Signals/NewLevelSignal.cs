using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals
{
    public class NewLevelSignal : ISignal
    {
        public int LevelNumber { get; }

        public NewLevelSignal(int value)
        {
            LevelNumber = value;
        }
    }
}
