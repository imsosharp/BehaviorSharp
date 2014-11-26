using System;

namespace BehaviorSharp.Components.Decorators
{
    public class Inverter : BehaviorComponent
    {
        private readonly BehaviorComponent _behavior;

        /// <summary>
        /// inverts the given behavior
        /// -Returns Success on Failure or Error
        /// -Returns Failure on Success 
        /// -Returns Running on Running
        /// </summary>
        /// <param name="behavior"></param>
        public Inverter(BehaviorComponent behavior)
        {
            _behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            try
            {
                switch (_behavior.Tick())
                {
                    case BehaviorState.Failure:
                        State = BehaviorState.Success;
                        return State;
                    case BehaviorState.Success:
                        State = BehaviorState.Failure;
                        return State;
                    case BehaviorState.Running:
                        State = BehaviorState.Running;
                        return State;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                State = BehaviorState.Success;
                return State;
            }

            State = BehaviorState.Success;
            return State;

        }

    }
}
