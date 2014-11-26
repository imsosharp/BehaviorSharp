#region LICENSE

// Copyright 2014 - 2014 BehaviorSharp
// UtilityVector.cs is part of BehaviorSharp.
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
using System.Linq;

#endregion

namespace BehaviorSharp.Components.Utility
{
    public class UtilityVector
    {
        public float[] Values { get; set; }

        //return the magnitude of this vector
        public float Magnitude
        {
            get { return (float) Math.Sqrt(Values.Sum(t => t*t)); }
        }

        public UtilityVector(params float[] values)
        {
            Values = values;
        }

        /// <summary>
        ///     Return a new vector based on the normalization of this instance.
        /// </summary>
        public UtilityVector Normalize()
        {
            if (Values.Length <= 0)
                return null;

            var vec = new UtilityVector {Values = new float[Values.Length]};
            Values.CopyTo(vec.Values, 0);

            for (var i = 0; i < vec.Values.Length; i++)
                vec.Values[i] = vec.Values[i]/vec.Magnitude;

            return vec;
        }

        /// <summary>
        ///     Dot between this and another specified vector. (based on normalized vectors)
        /// </summary>
        /// <param name="vector">Vector.</param>
        public float Dot(UtilityVector vector)
        {
            if (Magnitude == 0 || vector.Magnitude == 0)
                return -2;

            var a = Normalize();
            var b = vector.Normalize();

            return Values.Select((t, i) => a.Values[i]*b.Values[i]).Sum();
        }
    }
}