using System;

namespace BehaviorSharp.Components.Composites
{
    public class StatefulSequence : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;
        public int LastBehavior { get; private set; }

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
            for (; LastBehavior < _behaviors.Length; LastBehavior++)
            {
                try
                {
                    switch (_behaviors[LastBehavior].Tick())
                    {
                        case BehaviorState.Failure:
                            LastBehavior = 0;
                            State = BehaviorState.Failure;
                            return State;
                        case BehaviorState.Success:
                            continue;
                        case BehaviorState.Running:
                            State = BehaviorState.Running;
                            return State;
                        default:
                            LastBehavior = 0;
                            State = BehaviorState.Success;
                            return State;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                    LastBehavior = 0;
                    State = BehaviorState.Failure;
                    return State;
                }
            }

            LastBehavior = 0;
            State = BehaviorState.Success;
            return State;
        }


    }
}

