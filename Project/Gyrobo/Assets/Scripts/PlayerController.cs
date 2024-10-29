using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour
    {
        public float HorizontalInput;

        public float PlayerSpeed = 5;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            var distance = HorizontalInput * PlayerSpeed * Time.deltaTime;
            transform.Translate(Vector3.right * distance);

        }
    }
}
