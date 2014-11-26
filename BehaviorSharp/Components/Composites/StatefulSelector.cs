using System;

namespace BehaviorSharp.Components.Composites
{
    public class StatefulSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;
        public int LastBehavior { get; private set; }

        /// <summary>
        /// Selects among the given behavior components (stateful on running) 
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public StatefulSelector(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            for (; LastBehavior < _behaviors.Length; LastBehavior++)
            {
                try
                {
                    switch (_behaviors[LastBehavior].Tick())
                    {
                        case BehaviorState.Failure:
                            continue;
                        case BehaviorState.Success:
                            LastBehavior = 0;
                            State = BehaviorState.Success;
                            return State;
                        case BehaviorState.Running:
                            State = BehaviorState.Running;
                            return State;
                        default:
                            continue;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                }
            }

            LastBehavior = 0;
            State = BehaviorState.Failure;
            return State;
        }
    }
}
