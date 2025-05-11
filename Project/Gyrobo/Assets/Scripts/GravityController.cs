using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Gyrobo.Enums;
using UnityEngine;
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
            PlayerController.transform.Translate(Time.deltaTime * input * Constants.PlayerSpeed * direction);
        }

        public float x { get; private set; }
        public float y { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            PlayerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    x = 0f;
                    y = 20f;
                    GravityDirection = GravityDirection.Up;
                }

                else if ( Input.GetKey(KeyCode.LeftArrow))
                {
                    x = 20f;
                    y = 0f;
                    GravityDirection = GravityDirection.Left;
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    x = 0f;
                    y = -20f;
                    GravityDirection = GravityDirection.Down;
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    x = -20f;
                    y = 0f;
                    GravityDirection = GravityDirection.Right;
                }

                Physics.gravity = new Vector3(x, y, 0);
            }
           

        }
    }
}
