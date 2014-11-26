namespace BehaviorSharp.Components
{
    public abstract class BehaviorComponent
    {
        protected BehaviorReturnCode ReturnCode;

        public abstract BehaviorReturnCode Behave();
    }
}
