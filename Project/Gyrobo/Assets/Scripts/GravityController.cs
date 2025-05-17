using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Gyrobo.Enums;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Gyrobo
{
    public class GravityController : MonoBehaviour
    {
        public GravityDirection GravityDirection { get; private set; }

        public PlayerController PlayerController;
        
        public Vector3 JumpVelocity
        {
            get
            {
                switch (GravityDirection)
                {
                    
                    case GravityDirection.Left: 
                        return new Vector3(-Constants.JumpForce, 0, 0);
                    case GravityDirection.Up: 
                        return new Vector3(0, -Constants.JumpForce, 0);
                    case GravityDirection.Right: 
                        return new Vector3(Constants.JumpForce, 0, 0);
                    case GravityDirection.Down:
                    default:
                        return new Vector3(0, Constants.JumpForce, 0);
                }
            }
        }

        public void Jump(Rigidbody rigidBody)
        {
            rigidBody.AddForce(JumpVelocity * Constants.PlayerJump, ForceMode.Impulse);
        }

        public void Move()
        {
            var axis = GravityDirection == GravityDirection.Down || GravityDirection == GravityDirection.Up ? "Horizontal" : "Vertical";
            var direction = GravityDirection == GravityDirection.Down || GravityDirection == GravityDirection.Up ? Vector3.left : Vector3.up;
            var input = Input.GetAxis(axis);
            PlayerController.transform.Translate(Time.deltaTime * input * Constants.PlayerSpeed * Vector3.left);
        }

        public float GravityX { get; private set; }
        public float GravityY { get; private set; }

        // Start is called before the first frame update
        void Start() 
        {
            PlayerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!PlayerController.IsChangingGravity && Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    UpdateGravity(0f, 20f, GravityDirection.Up);
                }

                else if ( Input.GetKey(KeyCode.LeftArrow))
                {
                    UpdateGravity(20f, 0f, GravityDirection.Left);
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    UpdateGravity(0f, -20f, GravityDirection.Down);
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    UpdateGravity(-20f, 0f, GravityDirection.Right);
                }

                Physics.gravity = new Vector3(GravityX, GravityY, 0);
            }
            
            if (PlayerController.IsRotating)
            {
         
            }
        }

        public void UpdateGravity(float x, float y, GravityDirection gravityDirection)
        {
            this.GravityX = x;
            this.GravityY = y;

            if (GravityDirection != gravityDirection)
            {
                GravityDirection = gravityDirection;
                PlayerController.IsChangingGravity = true;
                PlayerController.transform.rotation = Quaternion.Euler(0f, 0f, (float)GravityDirection);
               

            }
        }
    }
}
