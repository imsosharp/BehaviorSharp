﻿using System;

namespace BehaviorSharp.Components.Utility
{
    public class UtilitySelector : BehaviorComponent
    {
        private readonly UtilityPair[] _utilityPairs;
        private readonly Func<UtilityVector> _utilityFunction;

        public UtilitySelector(Func<UtilityVector> utilityFunction, params UtilityPair[] pairs)
        {
            _utilityPairs = pairs;
            _utilityFunction = utilityFunction;
        }

        public override BehaviorState Tick()
        {
            try
            {
                var funcVector = _utilityFunction.Invoke();

                var min = -2.0f;
                UtilityPair bestMatch = null;

                //find max pair match
                foreach (var pair in _utilityPairs)
                {
                    var val = funcVector.Dot(pair.Vector);

                    if (val > min)
                    {
                        min = val;
                        bestMatch = pair;
                    }
                }

                //make sure we found a match
                if (bestMatch == null)
                {
#if DEBUG
                    Console.WriteLine("bestMatch not defined :(");
#endif
                    State = BehaviorState.Failure;
                    return State;
                }

                //execute best pair match and return result
                State = bestMatch.Behavior.Tick();
                return State;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e.ToString());
#endif
                State = BehaviorState.Failure;
                return BehaviorState.Failure;
            }
        }
    }
}