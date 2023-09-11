namespace Signals
{
    public class AddPointsSignal : ISignal
    {
        public float PointsValue { get; }

        public AddPointsSignal(float value)
        {
            PointsValue = value;
        }
    }
}