using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gyrobo;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    
    private GameObject _gameManagerObject;
    private GameManager _gameManager;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        _gameManagerObject = GameObject.FindGameObjectsWithTag(Constants.Tags.GameManager).FirstOrDefault();
        _gameManager = _gameManagerObject?.GetComponent<GameManager>();
        _playerController = _gameManager.player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.transform.tag.Equals(Constants.Tags.Player))
            {
                _playerController.DamagePlayer();
            }
        }
        else
        {
            lineRenderer.SetPosition(1, Constants.MaxLaserLength * Vector3.up);
        }
    }
}
