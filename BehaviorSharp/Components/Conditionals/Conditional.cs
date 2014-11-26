#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// Conditional.cs is part of BehaviorSharp.
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

namespace BehaviorSharp.Components.Conditionals
{
    public class Conditional : BehaviorComponent
    {
        private readonly Func<Boolean> _bool;

        /// <summary>
        ///     Returns a return code equivalent to the test
        ///     -Returns Success if true
        ///     -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
        public Conditional(Func<Boolean> test)
        {
            _bool = test;
        }

        /// <summary>
        ///     performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            try
            {
                switch (_bool.Invoke())
                {
                    case true:
                        State = BehaviorState.Success;
                        return State;
                    case false:
                        State = BehaviorState.Failure;
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