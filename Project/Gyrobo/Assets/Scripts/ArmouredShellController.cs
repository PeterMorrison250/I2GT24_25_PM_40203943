using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gyrobo;
using Gyrobo.Enums;
using UnityEngine;

public class ArmouredShellController : MonoBehaviour
{
    public Vector3 positionA;
    public Vector3 positionB;
    public bool IsMovingLeft = false;
    
    private GameObject _gameManagerObject;
    private GameManager _gameManager;
    private PlayerController _playerController;
    
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
        if (_gameManager.IsGameOver || _gameManager.IsLevelComplete || _gameManager.GravityDirection != GravityDirection.Down)
        {
            return;
        }
        
        if (IsMovingLeft)
        {
            Move(positionA);
            if (positionA == transform.position)
            {
                IsMovingLeft = false;
            }
        }
        else
        {
            Move(positionB);
            if (positionB == transform.position)
            {
                IsMovingLeft = true;
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (_gameManager.GravityDirection != GravityDirection.Down)
        {
            return;
        }
        
        if(collision.contacts.Length > 0)
        {
            if (collision.collider.tag.Equals(Constants.Tags.Player))
            {
                _playerController.DamagePlayer();
            }
        }
    }

    private void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Constants.EnemySpeed);
    }
}
