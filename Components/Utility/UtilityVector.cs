using System;
using System.Linq;

namespace BehaviorSharp.Components.Utility
{
    public class UtilityVector
    {
        public float[] Values { get; set; }

        //return the magnitude of this vector
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(Values.Sum(t => t * t));
            }
        }

        public UtilityVector(params float[] values)
        {
            Values = values;
        }

        /// <summary>
        /// Return a new vector based on the normalization of this instance.
        /// </summary>
        public UtilityVector Normalize()
        {
            if (Values.Length <= 0)
                return null;

            var vec = new UtilityVector { Values = new float[Values.Length] };
            Values.CopyTo(vec.Values, 0);

            for (var i = 0; i < vec.Values.Length; i++)
                vec.Values[i] = vec.Values[i] / vec.Magnitude;

            return vec;
        }

        /// <summary>
        /// Dot between this and another specified vector. (based on normalized vectors)
        /// </summary>
        /// <param name="vector">Vector.</param>
        public float Dot(UtilityVector vector)
        {
            if (Magnitude == 0 || vector.Magnitude == 0)
                return -2;

            var a = Normalize();
            var b = vector.Normalize();

            return Values.Select((t, i) => a.Values[i] * b.Values[i]).Sum();
        }
    }
}