using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gyrobo.Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gyrobo
{
    public class PlayerController : MonoBehaviour, IResetable
    {
        public event EventHandler HasDied;
        
        public bool IsJumping = false;
        public float airTime = 0f;
        
        private GameObject _gameManagerObject;
        private GameManager _gameManager;
        private GravityController _gravityController;
        private Rigidbody _rigidBody;

        // Start is called before the first frame update
        void Start()
        {
            _gameManagerObject = GameObject.FindGameObjectsWithTag(Constants.Tags.GameManager).FirstOrDefault();
            _gameManager = _gameManagerObject?.GetComponent<GameManager>();
            _gravityController = _gameManager?.GetComponent<GravityController>();
            GravityController.GravityChanged += HandleGravityChanged;
            _rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_gameManager.IsGameOver || _gameManager.IsLevelComplete)
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
        
        public void Reset()
        {
            IsJumping = false;
            airTime = 0;
            RotateToGravity(GravityDirection.Down);
        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.contacts.Length > 0)
            {
                var contact = collision.contacts[0];
                var dot = Vector3.Dot(contact.normal, _gameManager.GravityDirection.ToVector3().Invert());
                if(dot > 0.8 || dot < -0.8)
                {
                    IsJumping = false;
                    _gameManager.IsChangingGravity = false;

                    if (airTime > 1.5)
                    {
                        OnHasDied();
                    }
                    airTime = 0;
                }
            }
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
            airTime = 0;
            RotateToGravity(e.GravityDirection);
        }

        private void RotateToGravity(GravityDirection direction)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, (float)direction);
        }
    }
}
