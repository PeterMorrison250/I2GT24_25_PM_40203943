using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour
    {
        public float horizontalInput;
        public float verticalInput;

        public float playerSpeed = 5;

        public Rigidbody rigidBody;

        public GravityController gravityController;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            gravityController = GetComponent<GravityController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (gravityController.gravityDirection == Enums.GravityDirections.UP || gravityController.gravityDirection == Enums.GravityDirections.DOWN)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                var distance = horizontalInput * playerSpeed * Time.deltaTime;
                transform.Translate(Vector3.right * distance);
            } else
            {
                verticalInput = Input.GetAxis("Vertical");
                var distance = verticalInput * playerSpeed * Time.deltaTime;
                transform.Translate(Vector3.up * distance);
            }

            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ApplyGravityDirection();
            }
        }

        private void ApplyGravityDirection()
        {
            switch (gravityController.gravityDirection)
            {
                case Enums.GravityDirections.DOWN:
                    rigidBody.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                    break;
                case Enums.GravityDirections.RIGHT:
                    rigidBody.AddForce(new Vector3( -5, 0, 0), ForceMode.Impulse);
                    break;
                case Enums.GravityDirections.UP:
                    rigidBody.AddForce(new Vector3(0, -5, 0), ForceMode.Impulse);
                    break;
                case Enums.GravityDirections.LEFT:
                    rigidBody.AddForce(new Vector3(5, 0, 0), ForceMode.Impulse);
                    break;
                default:
                    break;
            }
        }
    }
}
