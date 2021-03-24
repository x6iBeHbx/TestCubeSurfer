using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MyTestWithAnim.Script.Utils
{
    public class MathUtil
    {
        public static float Round(float n, int d = 0)
        {
            if (d > 0)
            {
                float m = Mathf.Pow(10.0f, d);
                return Mathf.Round(n * m)/ m;
            }
            
            return Mathf.Round(n);
        }

        public static Vector3 Vector3ByYRotation(Vector3 vec, float angle)
        {
            float angleInRad = angle * Mathf.Deg2Rad;

            return new Vector3(
                Round(Mathf.Cos(angleInRad), 4) * vec.x + Round(Mathf.Sin(angleInRad), 4) * vec.z,
                vec.y,
                Round(-Mathf.Sin(angleInRad), 4)* vec.x + Round(Mathf.Cos(angleInRad), 4) * vec.z
                );
        }
    }
}
