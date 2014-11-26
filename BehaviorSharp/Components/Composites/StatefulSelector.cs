#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// StatefulSelector.cs is part of BehaviorSharp.
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
    public class StatefulSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;
        public int LastBehavior { get; private set; }

        /// <summary>
        ///     Selects among the given behavior components (stateful on running)
        ///     Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure
        ///     is certain
        ///     -Returns Success if a behavior component returns Success
        ///     -Returns Running if a behavior component returns Running
        ///     -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public StatefulSelector(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            for (; LastBehavior < _behaviors.Length; LastBehavior++)
            {
                try
                {
                    switch (_behaviors[LastBehavior].Tick())
                    {
                        case BehaviorState.Failure:
                            continue;
                        case BehaviorState.Success:
                            LastBehavior = 0;
                            State = BehaviorState.Success;
                            return State;
                        case BehaviorState.Running:
                            State = BehaviorState.Running;
                            return State;
                        default:
                            continue;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                }
            }

            LastBehavior = 0;
            State = BehaviorState.Failure;
            return State;
        }
    }
}