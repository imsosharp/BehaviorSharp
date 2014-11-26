#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Counter.cs is part of BehaviorSharp.
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
    public class Counter : BehaviorComponent
    {
        private readonly int _maxCount;
        private int _counter;
        private readonly BehaviorComponent _behavior;

        /// <summary>
        ///     executes the behavior based on a counter
        ///     -each time Counter is called the counter increments by 1
        ///     -Counter executes the behavior when it reaches the supplied maxCount
        /// </summary>
        /// <param name="maxCount">max number to count to</param>
        /// <param name="behavior">behavior to run</param>
        public Counter(int maxCount, BehaviorComponent behavior)
        {
            _maxCount = maxCount;
            _behavior = behavior;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            try
            {
                if (_counter < _maxCount)
                {
                    _counter++;
                    State = BehaviorState.Running;
                    return BehaviorState.Running;
                }

                _counter = 0;
                State = _behavior.Tick();
                return State;
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