#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// UtilityPair.cs is part of BehaviorSharp.
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

namespace BehaviorSharp.Components.Utility
{
    public class UtilityPair
    {
        public UtilityVector Vector { get; set; }
        public BehaviorComponent Behavior { get; set; }

        public UtilityPair(UtilityVector vector, BehaviorComponent behavior)
        {
            Vector = vector;
            Behavior = behavior;
        }
    }
}