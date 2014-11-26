namespace BehaviorSharp.Components
{
    public abstract class BehaviorComponent
    {
        protected BehaviorState State;

        public abstract BehaviorState Tick();
    }
}
