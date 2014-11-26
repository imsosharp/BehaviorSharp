#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Sequence.cs is part of BehaviorSharp.
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

namespace BehaviorSharp.Components.Composites
{
    public class Sequence : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;

        /// <summary>
        ///     attempts to run the behaviors all in one cycle
        ///     -Returns Success when all are successful
        ///     -Returns Failure if one behavior fails or an error occurs
        ///     -Returns Running if any are running
        /// </summary>
        /// <param name="behaviors"></param>
        public Sequence(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            //add watch for any running behaviors
            var anyRunning = false;

            foreach (var t in _behaviors)
            {
                try
                {
                    switch (t.Tick())
                    {
                        case BehaviorState.Failure:
                            State = BehaviorState.Failure;
                            return State;
                        case BehaviorState.Success:
                            continue;
                        case BehaviorState.Running:
                            anyRunning = true;
                            continue;
                        default:
                            State = BehaviorState.Success;
                            return State;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                    State = BehaviorState.Failure;
                    return State;
                }
            }

            //if none running, return success, otherwise return running
            State = !anyRunning ? BehaviorState.Success : BehaviorState.Running;
            return State;
        }
    }
}