namespace Signals
{
    public class OnAddPointsSignal : ISignal
    {
        public float PointsValue { get; }

        public OnAddPointsSignal(float value)
        {
            PointsValue = value;
        }
    }
}