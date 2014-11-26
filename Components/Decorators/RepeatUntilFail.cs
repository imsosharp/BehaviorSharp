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
        public override BehaviorReturnCode Behave()
        {
            ReturnCode = _behavior.Behave();
            if (ReturnCode == BehaviorReturnCode.Failure)
            {
                return BehaviorReturnCode.Failure;
            }

            ReturnCode = BehaviorReturnCode.Running;
            return BehaviorReturnCode.Running;
        }
    }
}
