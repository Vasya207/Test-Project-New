namespace Signals
{
    public class OnLevelStartSignal : ISignal
    {
        public int LevelNumber { get; }

        public OnLevelStartSignal(int value)
        {
            LevelNumber = value;
        }
    }
}