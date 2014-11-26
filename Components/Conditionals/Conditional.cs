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
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (_bool.Invoke())
                {
                    case true:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case false:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }
    }
}
