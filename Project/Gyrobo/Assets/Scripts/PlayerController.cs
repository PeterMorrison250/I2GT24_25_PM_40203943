using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour
    {
        public float horizontalInput;

        public Rigidbody rigidBody;
        
        public bool IsJumping = false;
        
        public Vector3 JumpVelocity = new Vector3(0, 10.0f, 0);

       // public GravityController gravityController;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        //    gravityController = GetComponent<GravityController>();
        }

        // Update is called once per frame
        void Update()
        {
            DetectMovement();
            DetectJump();
        }

        void OnCollisionEnter(Collision collision)
        {
            IsJumping = false;
        }

        private void DetectMovement()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Time.deltaTime * (horizontalInput * Constants.PlayerSpeed) * Vector3.left);
        }
        
        private void DetectJump()
        {
            if (!IsJumping && Input.GetKeyDown(KeyCode.Space))
            {
                IsJumping = true;
                rigidBody.AddForce(JumpVelocity * Constants.PlayerJump, ForceMode.Impulse);
            }
        }
        
        
        // private void ApplyGravityDirection()
        // {
        //     switch (gravityController.gravityDirection)
        //     {
        //         case Enums.GravityDirections.DOWN:
        //             rigidBody.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        //             break;
        //         case Enums.GravityDirections.RIGHT:
        //             rigidBody.AddForce(new Vector3( -5, 0, 0), ForceMode.Impulse);
        //             break;
        //         case Enums.GravityDirections.UP:
        //             rigidBody.AddForce(new Vector3(0, -5, 0), ForceMode.Impulse);
        //             break;
        //         case Enums.GravityDirections.LEFT:
        //             rigidBody.AddForce(new Vector3(5, 0, 0), ForceMode.Impulse);
        //             break;
        //         default:
        //             break;
        //     }
        // }
    }
}
