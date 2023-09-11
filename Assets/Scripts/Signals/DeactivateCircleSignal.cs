using CircleComponents;

namespace Signals
{
    public class DeactivateCircleSignal : ISignal
    {
        public Circle CircleObj { get; }
        
        public DeactivateCircleSignal(Circle circleInst)
        {
            CircleObj = circleInst;
        }
    }
}