using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _gameManagerObject;
        private GameManager _gameManager;
        private GravityController _gravityController;
        public event EventHandler HasDied;
        
        public bool IsJumping = false;
        public float airTime = 0f;

        private Rigidbody _rigidBody;

        // Start is called before the first frame update
        void Start()
        {
            _gameManagerObject = GameObject.FindGameObjectsWithTag("GameManager").FirstOrDefault();
            _gameManager = _gameManagerObject?.GetComponent<GameManager>();
            _gravityController = _gameManager?.GetComponent<GravityController>();
            _gravityController.GravityChanged += HandleGravityChanged;
            _rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_gameManager.IsGameOver)
            {
                return;
            }
            
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
            _gameManager.IsChangingGravity = false;

            if (airTime > 1.2)
            {
                OnHasDied();
            }
            airTime = 0;
        }

        private void DetectMovement()
        {
            _gravityController.Move();
        }
        
        private void DetectJump()
        {
            if (!IsJumping && Input.GetKeyDown(KeyCode.Space))
            {
                IsJumping = true; 
                _gravityController.Jump(_rigidBody);
            }

            if (IsJumping || _gameManager.IsChangingGravity)
            {
                airTime += Time.deltaTime;
            }
        }

        private void OnHasDied()
        {
            HasDied?.Invoke(this, EventArgs.Empty);
        }

        private void HandleGravityChanged(object sender, GravityChangedEventArgs e)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, (float)e.GravityDirection);
        }
    }
}
