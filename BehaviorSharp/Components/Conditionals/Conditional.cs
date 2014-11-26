using System;

namespace BehaviorSharp.Components.Conditionals
{
    public class Conditional : BehaviorComponent
    {
        private readonly Func<Boolean> _bool;

        /// <summary>
        /// Returns a return code equivalent to the test 
        /// -Returns Success if true
        /// -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
        public Conditional(Func<Boolean> test)
        {
            _bool = test;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            try
            {
                switch (_bool.Invoke())
                {
                    case true:
                        State = BehaviorState.Success;
                        return State;
                    case false:
                        State = BehaviorState.Failure;
                        return State;
                    default:
                        State = BehaviorState.Failure;
                        return State;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                State = BehaviorState.Failure;
                return State;
            }
        }
    }
}
