using System;

namespace BehaviorSharp.Components.Composites
{
    public class StatefulSequence : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;
        private int _lastBehavior;

        /// <summary>
        /// attempts to run the behaviors all in one cycle (stateful on running)
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Does not Return Running
        /// </summary>
        /// <param name="behaviors"></param>
        public StatefulSequence(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            //start from last remembered position
            for (; _lastBehavior < _behaviors.Length; _lastBehavior++)
            {
                try
                {
                    switch (_behaviors[_lastBehavior].Tick())
                    {
                        case BehaviorState.Failure:
                            _lastBehavior = 0;
                            State = BehaviorState.Failure;
                            return State;
                        case BehaviorState.Success:
                            continue;
                        case BehaviorState.Running:
                            State = BehaviorState.Running;
                            return State;
                        default:
                            _lastBehavior = 0;
                            State = BehaviorState.Success;
                            return State;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                    _lastBehavior = 0;
                    State = BehaviorState.Failure;
                    return State;
                }
            }

            _lastBehavior = 0;
            State = BehaviorState.Success;
            return State;
        }


    }
}

