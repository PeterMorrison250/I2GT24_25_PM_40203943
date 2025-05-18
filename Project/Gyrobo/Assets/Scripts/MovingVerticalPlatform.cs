using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gyrobo;
using Gyrobo.Enums;
using UnityEngine;

public class MovingVerticalPlatform : MonoBehaviour
{

    public float maxY;
    public float minY;

    private GameObject _gameManagerObject;
    private GameManager _gameManager;

    public GravityController gravityController;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManagerObject = GameObject.FindGameObjectsWithTag("GameManager").FirstOrDefault();
        _gameManager = _gameManagerObject.GetComponent<GameManager>();

        gravityController = _gameManagerObject?.GetComponent<GravityController>();
    }

    // Update is called once per frame
    void Update()
    {
            switch (_gameManager.GravityDirection)
            {
                case GravityDirection.Up:
                    if (transform.position.y < maxY)
                    {
                        MovePlatform(Vector3.up);
                    }
                    break;
                case GravityDirection.Down:
                    if (transform.position.y > minY)
                    {
                        MovePlatform(Vector3.down);
                    }
                    break;
            
        }
    }

    private void MovePlatform(Vector3 direction)
    {
        transform.Translate(Time.deltaTime * Constants.PlatformSpeed * direction);
    }
}
