#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// UtilitySelector.cs is part of BehaviorSharp.
// BehaviorSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// BehaviorSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with BehaviorSharp. If not, see <http://www.gnu.org/licenses/>.

#endregion

#region

using System;

#endregion

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