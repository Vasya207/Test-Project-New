namespace Signals
{
    public class OnAddPointsSignal : ISignal
    {
        public float Parameter { get; }

        public OnAddPointsSignal(float value)
        {
            Parameter = value;
        }
    }
}