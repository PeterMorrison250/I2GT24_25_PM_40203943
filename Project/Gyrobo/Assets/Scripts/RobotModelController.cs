using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gyrobo;
using Gyrobo.Enums;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject _gameManagerObject;
    private GameManager _gameManager;
    private GravityController _gravityController;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManagerObject = GameObject.FindGameObjectsWithTag(Constants.Tags.GameManager).FirstOrDefault();
        _gameManager = _gameManagerObject?.GetComponent<GameManager>();
        _gravityController = _gameManager?.GetComponent<GravityController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.GravityDirection == GravityDirection.Down)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                _animator.SetBool("IsWalking", true);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                _animator.SetBool("IsWalking", true);
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                _animator.SetBool("IsWalking", false);
            }
        }

        if (_gameManager.GravityDirection == GravityDirection.Up)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0, -90f, -180f);
                _animator.SetBool("IsWalking", true);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0f, 90f, -180f);
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }

        if (_gameManager.GravityDirection == GravityDirection.Left)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.rotation = Quaternion.Euler(-90f, -33, 125);
                _animator.SetBool("IsWalking", true);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.rotation = Quaternion.Euler(90, -180f, 270);
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
        
        if (_gameManager.GravityDirection == GravityDirection.Right)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.rotation = Quaternion.Euler(-90f, -90, 0);
                _animator.SetBool("IsWalking", true);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.rotation = Quaternion.Euler(-270, -315, -45);
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
        
    }
}
