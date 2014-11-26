#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// RandomDecorator.cs is part of BehaviorSharp.
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

namespace BehaviorSharp.Components.Decorators
{
    public class RandomDecorator : BehaviorComponent
    {
        private readonly float _probability;
        private readonly Func<float> _randomFunction;
        private readonly BehaviorComponent _behavior;

        /// <summary>
        ///     randomly executes the behavior
        /// </summary>
        /// <param name="probability">probability of execution</param>
        /// <param name="randomFunction">function that determines probability to execute</param>
        /// <param name="behavior">behavior to execute</param>
        public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
        {
            _probability = probability;
            _randomFunction = randomFunction;
            _behavior = behavior;
        }

        public override BehaviorState Tick()
        {
            try
            {
                if (_randomFunction.Invoke() <= _probability)
                {
                    State = _behavior.Tick();
                    return State;
                }

                State = BehaviorState.Running;
                return BehaviorState.Running;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                State = BehaviorState.Failure;
                return BehaviorState.Failure;
            }
        }
    }
}