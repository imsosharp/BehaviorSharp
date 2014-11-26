using System;
using BehaviorSharp.Components;
using BehaviorSharp.Components.Composites;

namespace BehaviorSharp
{
    public enum BehaviorState
    {
        Failure,
        Success,
        Running
    }

    public delegate BehaviorState BehaviorReturn();

    /// <summary>
    /// 
    /// </summary>
    public class Behavior
    {
        public BehaviorState State { get; set; }

        private readonly BehaviorComponent _root;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public Behavior(IndexSelector root)
        {
            _root = root;
        }

        public Behavior(BehaviorComponent root)
        {
            _root = root;
        }

        /// <summary>
        /// perform the behavior
        /// </summary>
        public BehaviorState Tick()
        {
            try
            {
                switch (_root.Tick())
                {
                    case BehaviorState.Failure:
                        State = BehaviorState.Failure;
                        return State;
                    case BehaviorState.Success:
                        State = BehaviorState.Success;
                        return State;
                    case BehaviorState.Running:
                        State = BehaviorState.Running;
                        return State;
                    default:
                        State = BehaviorState.Running;
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
