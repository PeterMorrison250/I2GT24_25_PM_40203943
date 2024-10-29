using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour
    {
        public float horizontalInput;

        public float playerSpeed = 5;

        public Rigidbody rigidBody;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            var distance = horizontalInput * playerSpeed * Time.deltaTime;
            transform.Translate(Vector3.right * distance);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.AddForce(new Vector3(0,5,0), ForceMode.Impulse);
            }
        }
    }
}
