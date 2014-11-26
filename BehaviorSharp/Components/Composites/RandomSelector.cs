#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// RandomSelector.cs is part of BehaviorSharp.
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
    public class RandomSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;
        private Random _random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        ///     Randomly selects and performs one of the passed behaviors
        ///     -Returns Success if selected behavior returns Success
        ///     -Returns Failure if selected behavior returns Failure
        ///     -Returns Running if selected behavior returns Running
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public RandomSelector(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            _random = new Random(DateTime.Now.Millisecond);

            try
            {
                switch (_behaviors[_random.Next(0, _behaviors.Length - 1)].Tick())
                {
                    case BehaviorState.Failure:
                        State = BehaviorState.Failure;
                        return State;
                    case BehaviorState.Success:
                        State = BehaviorState.Success;
                        return State;
                    case BehaviorState.Running:
                        State = BehaviorState.Running;
                        return State;
                    default:
                        State = BehaviorState.Failure;
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
    }
}