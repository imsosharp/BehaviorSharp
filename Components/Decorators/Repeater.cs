namespace BehaviorSharp.Components.Decorators
{
    public class Repeater : BehaviorComponent
    {
        private readonly BehaviorComponent _behavior;

        /// <summary>
        /// executes the behavior every time again
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Repeater(BehaviorComponent behavior)
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
            ReturnCode = BehaviorReturnCode.Running;
            return BehaviorReturnCode.Running;
        }
    }
}
