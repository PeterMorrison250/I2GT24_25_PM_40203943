using System.Collections;
using System.Collections.Generic;
using Gyrobo.Enums;
using UnityEngine;

namespace Gyrobo
{
    public class GravityController : MonoBehaviour
    {
        public GravityDirection gravityDirection { get; private set; }

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
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    x = 0f;
                    y = 20f;
                    gravityDirection = GravityDirection.Up;
                }

                else if ( Input.GetKey(KeyCode.LeftArrow))
                {
                    x = 20f;
                    y = 0f;
                    gravityDirection = GravityDirection.Left;
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    x = 0f;
                    y = -20f;
                    gravityDirection = GravityDirection.Down;
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    x = -20f;
                    y = 0f;
                    gravityDirection = GravityDirection.Right;
                }

                Physics.gravity = new Vector3(x, y, 0);
            }
           

        }
    }
}
