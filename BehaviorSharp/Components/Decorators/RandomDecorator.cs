using System;

namespace BehaviorSharp.Components.Decorators
{
    public class RandomDecorator : BehaviorComponent
    {
        private readonly float _probability;
        private readonly Func<float> _randomFunction;
        private readonly BehaviorComponent _behavior;

        /// <summary>
        /// randomly executes the behavior
        /// </summary>
        /// <param name="probability">probability of execution</param>
        /// <param name="randomFunction">function that determines probability to execute</param>
        /// <param name="behavior">behavior to execute</param>
        public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
        {
            _probability = probability;
            _randomFunction = randomFunction;
            _behavior = behavior;
        }

        public override BehaviorState Tick()
        {
            try
            {
                if (_randomFunction.Invoke() <= _probability)
                {
                    State = _behavior.Tick();
                    return State;
                }

                State = BehaviorState.Running;
                return BehaviorState.Running;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                State = BehaviorState.Failure;
                return BehaviorState.Failure;
            }
        }
    }
}
