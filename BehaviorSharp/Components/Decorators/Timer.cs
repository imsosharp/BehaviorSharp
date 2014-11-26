#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Timer.cs is part of BehaviorSharp.
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
    public class Timer : BehaviorComponent
    {
        private readonly Func<int> _elapsedTimeFunction;
        private readonly BehaviorComponent _behavior;
        private int _timeElapsed;
        private readonly int _waitTime;

        /// <summary>
        ///     executes the behavior after a given amount of time in miliseconds has passed
        /// </summary>
        /// <param name="elapsedTimeFunction">function that returns elapsed time</param>
        /// <param name="timeToWait">maximum time to wait before executing behavior</param>
        /// <param name="behavior">behavior to run</param>
        public Timer(Func<int> elapsedTimeFunction, int timeToWait, BehaviorComponent behavior)
        {
            _elapsedTimeFunction = elapsedTimeFunction;
            _behavior = behavior;
            _waitTime = timeToWait;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            try
            {
                _timeElapsed += _elapsedTimeFunction.Invoke();

                if (_timeElapsed >= _waitTime)
                {
                    _timeElapsed = 0;
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