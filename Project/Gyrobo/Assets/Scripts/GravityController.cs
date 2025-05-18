using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Gyrobo.Enums;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Gyrobo
{
    public class GravityController : MonoBehaviour, IResetable
    {
        public static event GravityChangedEventHandler GravityChanged;
        
        private GameObject _gameManagerObject;
        private GameManager _gameManager;
        private PlayerController _playerController;
        
        public float GravityX { get; private set; }
        public float GravityY { get; private set; }
        public Vector3 JumpVelocity
        {
            get
            {
                switch (_gameManager.GravityDirection)
                {
                    
                    case GravityDirection.Left: 
                        return new Vector3(-Constants.JumpForce, 0, 0);
                    case GravityDirection.Up: 
                        return new Vector3(0, -Constants.JumpForce, 0);
                    case GravityDirection.Right: 
                        return new Vector3(Constants.JumpForce, 0, 0);
                    case GravityDirection.Down:
                    default:
                        return new Vector3(0, Constants.JumpForce, 0);
                }
            }
        }
        
        // Start is called before the first frame update
        void Start() 
        {
            _gameManagerObject = GameObject.FindGameObjectsWithTag(Constants.Tags.GameManager).FirstOrDefault();
            _gameManager = _gameManagerObject?.GetComponent<GameManager>();
            _playerController = _gameManager?.player.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_gameManager.IsChangingGravity && Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    UpdateGravity(0f, 20f, GravityDirection.Up);
                }

                else if ( Input.GetKey(KeyCode.LeftArrow))
                {
                    UpdateGravity(20f, 0f, GravityDirection.Left);
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    UpdateGravity(0f, -20f, GravityDirection.Down);
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    UpdateGravity(-20f, 0f, GravityDirection.Right);
                }

                if (GravityX is 0 && GravityY is 0)
                {
                    return;
                }

                Physics.gravity = new Vector3(GravityX, GravityY, 0);
            }
        }
        
        public void Jump(Rigidbody rigidBody)
        {
            rigidBody.AddForce(JumpVelocity * Constants.PlayerJump, ForceMode.Impulse);
        }

        public void Move()
        {
            var axis = _gameManager.GravityDirection == GravityDirection.Down || _gameManager.GravityDirection == GravityDirection.Up ? "Horizontal" : "Vertical";
            var direction = _gameManager.GravityDirection == GravityDirection.Down || _gameManager.GravityDirection == GravityDirection.Right ? Vector3.left : Vector3.right;
            
            var input = Input.GetAxis(axis);
            _playerController.transform.Translate(Time.deltaTime * input * Constants.PlayerSpeed * direction);
        }
        
        public void Reset()
        {
            UpdateGravity(0f, -20f, GravityDirection.Down);
        }
        
        private void UpdateGravity(float x, float y, GravityDirection gravityDirection)
        {
            this.GravityX = x;
            this.GravityY = y;

            if (_gameManager.GravityDirection != gravityDirection)
            {
                _gameManager.GravityDirection = gravityDirection;
                _gameManager.IsChangingGravity = true;
                OnGravityChanged(gravityDirection);
            }
        }
        
        private void OnGravityChanged(GravityDirection gravityDirection)
        {
            GravityChanged?.Invoke(this, new GravityChangedEventArgs() { GravityDirection = gravityDirection });
        }
    }
}
