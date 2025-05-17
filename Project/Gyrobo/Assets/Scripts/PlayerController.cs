using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour
    {
        public event EventHandler HasDied;
        
        public float horizontalInput;

        public Rigidbody rigidBody;
        
        public bool IsJumping = false;

        public float airTime = 0f;


        public bool IsChangingGravity { get; set; }


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
            
            DetectMovement();
            DetectJump();
        }

        public void DamagePlayer()
        {
            OnHasDied();
        }

        void OnCollisionEnter(Collision collision)
        {
            IsJumping = false;
            IsChangingGravity = false;

            if (airTime > 1.2)
            {
                OnHasDied();
            }
            airTime = 0;
        }

        private void DetectMovement()
        {
            gravityController.Move();
        }
        
        private void DetectJump()
        {
            if (!IsJumping && Input.GetKeyDown(KeyCode.Space))
            {
                IsJumping = true; 
                gravityController.Jump(rigidBody);
            }

            if (IsJumping || IsChangingGravity)
            {
                airTime += Time.deltaTime;
            }
        }

        private void OnHasDied()
        {
            HasDied?.Invoke(this, EventArgs.Empty);
        }
    }
}
