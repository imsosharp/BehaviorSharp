namespace BehaviorSharp.Components.Utility
{
    public class UtilityPair
    {
        public UtilityVector Vector { get; set; }
        public BehaviorComponent Behavior { get; set; }

        public UtilityPair(UtilityVector vector, BehaviorComponent behavior)
        {
            Vector = vector;
            Behavior = behavior;
        }
    }
}
