#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Inverter.cs is part of BehaviorSharp.
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
    public class Inverter : BehaviorComponent
    {
        private readonly BehaviorComponent _behavior;

        /// <summary>
        ///     inverts the given behavior
        ///     -Returns Success on Failure or Error
        ///     -Returns Failure on Success
        ///     -Returns Running on Running
        /// </summary>
        /// <param name="behavior"></param>
        public Inverter(BehaviorComponent behavior)
        {
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
                switch (_behavior.Tick())
                {
                    case BehaviorState.Failure:
                        State = BehaviorState.Success;
                        return State;
                    case BehaviorState.Success:
                        State = BehaviorState.Failure;
                        return State;
                    case BehaviorState.Running:
                        State = BehaviorState.Running;
                        return State;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                State = BehaviorState.Success;
                return State;
            }

            State = BehaviorState.Success;
            return State;
        }
    }
}