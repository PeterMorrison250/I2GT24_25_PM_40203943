using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private float turretRange;

    private Transform playerPosition;
    
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (!IsInRange)
        {
            return;
        }
        
        TurnToPlayer();
    }

    private bool IsInRange => Vector3.Distance(playerPosition.position, transform.position) <= turretRange;

    private void TurnToPlayer()
    {
        var relativePosition = playerPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
        transform.rotation = rotation;
    }
}
