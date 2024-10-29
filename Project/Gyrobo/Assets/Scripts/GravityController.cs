using System.Collections;
using System.Collections.Generic;
using Gyrobo.Enums;
using UnityEngine;

namespace Gyrobo
{
    public class GravityController : MonoBehaviour
    {
        public GravityDirections gravityDirection { get; private set; }

        public float x { get; private set; }
        public float y { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    x = 0f;
                    y = 9.8f;
                    gravityDirection = GravityDirections.UP;
                }

                else if ( Input.GetKey(KeyCode.A))
                {
                    x = -9.8f;
                    y = 0f;
                    gravityDirection = GravityDirections.LEFT;
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    x = 0f;
                    y = -9.8f;
                    gravityDirection = GravityDirections.DOWN;
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    x = 9.8f;
                    y = 0f;
                    gravityDirection = GravityDirections.RIGHT;
                }

                Physics.gravity = new Vector3(x, y, 0);
            }
           

        }
    }
}
