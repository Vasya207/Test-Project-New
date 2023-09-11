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
