#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// IndexSelector.cs is part of BehaviorSharp.
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
    public class IndexSelector : BehaviorComponent
    {
        private readonly BehaviorComponent[] _behaviors;
        private readonly Func<int> _index;

        /// <summary>
        ///     The selector for the root node of the behavior tree
        /// </summary>
        /// <param name="index">an index representing which of the behavior branches to perform</param>
        /// <param name="behaviors">the behavior branches to be selected from</param>
        public IndexSelector(Func<int> index, params BehaviorComponent[] behaviors)
        {
            _index = index;
            _behaviors = behaviors;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            try
            {
                switch (_behaviors[_index.Invoke()].Tick())
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
                        State = BehaviorState.Running;
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