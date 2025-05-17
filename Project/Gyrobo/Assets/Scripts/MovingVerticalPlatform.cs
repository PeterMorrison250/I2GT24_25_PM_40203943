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

    private GameObject _gameManager;

    public GravityController gravityController;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectsWithTag("GameManager").FirstOrDefault();

        gravityController = _gameManager?.GetComponent<GravityController>();
    }

    // Update is called once per frame
    void Update()
    {
            switch (gravityController.GravityDirection)
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
