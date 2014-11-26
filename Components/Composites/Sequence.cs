using System;

namespace BehaviorSharp.Components.Composites
{
    public class Sequence : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;

        /// <summary>
        /// attempts to run the behaviors all in one cycle
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Returns Running if any are running
        /// </summary>
        /// <param name="behaviors"></param>
        public Sequence(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            //add watch for any running behaviors
            var anyRunning = false;

            foreach (var t in _behaviors)
            {
                try
                {
                    switch (t.Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            continue;
                        case BehaviorReturnCode.Running:
                            anyRunning = true;
                            continue;
                        default:
                            ReturnCode = BehaviorReturnCode.Success;
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

            //if none running, return success, otherwise return running
            ReturnCode = !anyRunning ? BehaviorReturnCode.Success : BehaviorReturnCode.Running;
            return ReturnCode;
        }


    }
}
