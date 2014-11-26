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

        public override BehaviorReturnCode Behave()
        {
            try
            {
                if (_randomFunction.Invoke() <= _probability)
                {
                    ReturnCode = _behavior.Behave();
                    return ReturnCode;
                }

                ReturnCode = BehaviorReturnCode.Running;
                return BehaviorReturnCode.Running;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return BehaviorReturnCode.Failure;
            }
        }
    }
}
