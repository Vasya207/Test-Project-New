using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals
{
    public class OnNewLevelSignal : ISignal
    {
        public int Parameter { get; }

        public OnNewLevelSignal(int value)
        {
            Parameter = value;
        }
    }
}
