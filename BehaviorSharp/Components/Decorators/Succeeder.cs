#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Succeeder.cs is part of BehaviorSharp.
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

namespace BehaviorSharp.Components.Decorators
{
    public class Succeeder : BehaviorComponent
    {
        private readonly BehaviorComponent _behavior;

        /// <summary>
        ///     returns a success even when the decorated component failed
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Succeeder(BehaviorComponent behavior)
        {
            _behavior = behavior;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            State = _behavior.Tick();
            if (State == BehaviorState.Failure)
            {
                State = BehaviorState.Success;
            }
            return State;
        }
    }
}