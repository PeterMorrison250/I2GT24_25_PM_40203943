using Gyrobo.Enums;
using UnityEngine;

namespace Gyrobo
{
    public static class GravityExtensions
    {
        public static Vector3 ToVector3(this GravityDirection gravityDirection)
        {
            switch (gravityDirection)
            {
                case GravityDirection.Up:
                    return Vector3.up;
                case GravityDirection.Down:
                    return Vector3.down;
                case GravityDirection.Left:
                    return Vector3.left;
                case GravityDirection.Right:
                    return Vector3.right;
            }
            return Vector3.zero;
        }

        public static Vector3 Invert(this Vector3 vector)
        {
            return -vector;
        }
    }
}