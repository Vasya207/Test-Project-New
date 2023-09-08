namespace Signals
{
    public class OnDeactivateCircleSignal : ISignal
    {
        public Circle CircleObj { get; }
        
        public OnDeactivateCircleSignal(Circle circleInst)
        {
            CircleObj = circleInst;
        }
    }
}