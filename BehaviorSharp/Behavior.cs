#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Behavior.cs is part of BehaviorSharp.
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
using BehaviorSharp.Components;
using BehaviorSharp.Components.Composites;

#endregion

namespace BehaviorSharp
{
    public enum BehaviorState
    {
        Failure,
        Success,
        Running
    }

    public delegate BehaviorState BehaviorReturn();

    /// <summary>
    ///     Fork
    ///     https://github.com/NetGnome/BehaviorLibrary
    /// </summary>
    public class Behavior
    {
        public BehaviorState State { get; set; }

        private readonly BehaviorComponent _root;

        /// <summary>
        /// </summary>
        /// <param name="root"></param>
        public Behavior(IndexSelector root)
        {
            _root = root;
        }

        public Behavior(BehaviorComponent root)
        {
            _root = root;
        }

        /// <summary>
        ///     perform the behavior
        /// </summary>
        public BehaviorState Tick()
        {
            try
            {
                switch (_root.Tick())
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