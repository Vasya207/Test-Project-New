using UnityEngine.Events;

namespace Core
{
    public static class Signals
    {
        public static UnityEvent<int> OnNewLevel = new();
        public static UnityEvent<int> OnLevelStart = new();
    }
}