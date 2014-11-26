using System;

namespace BehaviorSharp.Components.Composites
{
    public class Selector : BehaviorComponent
    {
        protected BehaviorComponent[] Behaviors;

        /// <summary>
        /// Selects among the given behavior components
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public Selector(params BehaviorComponent[] behaviors)
        {
            Behaviors = behaviors;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            foreach (var t in Behaviors)
            {
                try
                {
                    switch (t.Tick())
                    {
                        case BehaviorState.Failure:
                            continue;
                        case BehaviorState.Success:
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

            State = BehaviorState.Failure;
            return State;
        }
    }
}
