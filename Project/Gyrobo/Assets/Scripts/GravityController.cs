using System.Collections;
using System.Collections.Generic;
using Gyrobo.Enums;
using UnityEngine;

namespace Gyrobo
{
    public class GravityController : MonoBehaviour
    {
        public GravityDirections GravityDirection { get; private set; }

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
                    Physics.gravity = new Vector3(0, 9.8f, 0);
                }

                else if ( Input.GetKey(KeyCode.A))
                {
                    Physics.gravity = new Vector3(-9.8f, 0, 0);
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    Physics.gravity = new Vector3(0, -9.8f, 0);
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    Physics.gravity = new Vector3(9.8f, 0, 0);
                }
            }
           

        }
    }
}
