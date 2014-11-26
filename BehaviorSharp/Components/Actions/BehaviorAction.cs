using System;

namespace BehaviorSharp.Components.Actions
{
    public class BehaviorAction : BehaviorComponent
    {
        private readonly Func<BehaviorState> _action;

        public BehaviorAction(Func<BehaviorState> action)
        {
            _action = action;
        }

        public override BehaviorState Tick()
        {
            try
            {
                switch (_action.Invoke())
                {
                    case BehaviorState.Success:
                        State = BehaviorState.Success;
                        return State;
                    case BehaviorState.Failure:
                        State = BehaviorState.Failure;
                        return State;
                    case BehaviorState.Running:
                        State = BehaviorState.Running;
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
