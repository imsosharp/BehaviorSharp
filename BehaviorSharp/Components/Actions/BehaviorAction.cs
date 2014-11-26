#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// BehaviorAction.cs is part of BehaviorSharp.
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

namespace BehaviorSharp.Components.Actions
{
    public class BehaviorAction : BehaviorComponent
    {
        private readonly Func<BehaviorState> _action;

        public BehaviorAction(Func<BehaviorState> action)
        {
            _action = action;
        }

        public override BehaviorState Tick()
        {
            try
            {
                switch (_action.Invoke())
                {
                    case BehaviorState.Success:
                        State = BehaviorState.Success;
                        return State;
                    case BehaviorState.Failure:
                        State = BehaviorState.Failure;
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