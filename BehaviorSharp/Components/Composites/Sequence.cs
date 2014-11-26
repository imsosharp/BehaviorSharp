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
        public override BehaviorState Tick()
        {
            //add watch for any running behaviors
            var anyRunning = false;

            foreach (var t in _behaviors)
            {
                try
                {
                    switch (t.Tick())
                    {
                        case BehaviorState.Failure:
                            State = BehaviorState.Failure;
                            return State;
                        case BehaviorState.Success:
                            continue;
                        case BehaviorState.Running:
                            anyRunning = true;
                            continue;
                        default:
                            State = BehaviorState.Success;
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

            //if none running, return success, otherwise return running
            State = !anyRunning ? BehaviorState.Success : BehaviorState.Running;
            return State;
        }


    }
}
