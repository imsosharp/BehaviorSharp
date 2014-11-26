#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Selector.cs is part of BehaviorSharp.
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
    public class Selector : BehaviorComponent
    {
        protected BehaviorComponent[] Behaviors;

        /// <summary>
        ///     Selects among the given behavior components
        ///     Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure
        ///     is certain
        ///     -Returns Success if a behavior component returns Success
        ///     -Returns Running if a behavior component returns Running
        ///     -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public Selector(params BehaviorComponent[] behaviors)
        {
            Behaviors = behaviors;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            foreach (var t in Behaviors)
            {
                try
                {
                    switch (t.Tick())
                    {
                        case BehaviorState.Failure:
                            continue;
                        case BehaviorState.Success:
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

            State = BehaviorState.Failure;
            return State;
        }
    }
}