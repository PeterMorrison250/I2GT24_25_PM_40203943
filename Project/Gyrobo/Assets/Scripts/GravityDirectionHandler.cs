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

        public static Vector3 TrackAlongSurface(GravityDirection gravityDirection, Transform transform, Transform targetTransform)
        {
            switch (gravityDirection)
            {
                case GravityDirection.Down:
                case GravityDirection.Up:
                    return new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
                case GravityDirection.Right:
                    return new Vector3(transform.position.x, targetTransform.position.y, targetTransform.position.z);
                case GravityDirection.Left:
                    return new Vector3(transform.position.x, targetTransform.position.y, targetTransform.position.z);
                default: return new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
            }
        }

        public static float GetFloorPointValueDependentOnGravity(GravityDirection gravityDirection, Vector3 point)
        {
            switch (gravityDirection)
            {
                case GravityDirection.Down:
                case GravityDirection.Up:
                    return point.y;
                case GravityDirection.Right:
                case GravityDirection.Left:
                    return point.x;
                default: return point.y;
            }
        }
    }
}