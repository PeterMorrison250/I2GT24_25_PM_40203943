using Gyrobo.Enums;
using UnityEngine;

namespace Gyrobo
{
    public static class GravityDirectionHandler
    {
        public static Quaternion Face(GravityDirection gravityDirection, FacingDirection facingDirection)
        {
            switch (gravityDirection)
            {
                case GravityDirection.Down:
                    if (facingDirection == FacingDirection.Left)
                    {
                        return Quaternion.Euler(0f, 90f, 0f);
                    }

                    return Quaternion.Euler(0f, 270f, 0f);

                case GravityDirection.Up:
                    if (facingDirection == FacingDirection.Left)
                    {
                        return Quaternion.Euler(0f, 90f, -180f);
                    }

                    return Quaternion.Euler(0f, 270f, -180f);

                case GravityDirection.Right:
                    if (facingDirection == FacingDirection.Left)
                    {
                        return Quaternion.Euler(90f, 0f, -90f);
                    }

                    return Quaternion.Euler(-90f, 0f, -90f);

                case GravityDirection.Left:
                    if (facingDirection == FacingDirection.Left)
                    {
                        return Quaternion.Euler(-90f, 180f, -90f);
                    }

                    return Quaternion.Euler(90f, 180f, -90f);

                default:
                    return default;
            }
        }
    }
}