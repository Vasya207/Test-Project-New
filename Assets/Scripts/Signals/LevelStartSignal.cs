namespace Signals
{
    public class LevelStartSignal : ISignal
    {
        public int LevelNumber { get; }

        public LevelStartSignal(int value)
        {
            LevelNumber = value;
        }
    }
}