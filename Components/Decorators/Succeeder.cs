namespace BehaviorSharp.Components.Decorators
{
    public class Succeeder : BehaviorComponent
    {
        private readonly BehaviorComponent _behavior;

        /// <summary>
        /// returns a success even when the decorated component failed
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Succeeder(BehaviorComponent behavior)
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
                ReturnCode = BehaviorReturnCode.Success;
            }
            return ReturnCode;
        }
    }
}
