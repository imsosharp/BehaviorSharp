namespace BehaviorSharp.Components.Decorators
{
    public class RepeatUntilFail : BehaviorComponent
    {
        private readonly BehaviorComponent _behavior;

        /// <summary>
        /// executes the behavior every time again
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public RepeatUntilFail(BehaviorComponent behavior)
        {
            _behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            State = _behavior.Tick();
            if (State == BehaviorState.Failure)
            {
                return BehaviorState.Failure;
            }

            State = BehaviorState.Running;
            return BehaviorState.Running;
        }
    }
}
