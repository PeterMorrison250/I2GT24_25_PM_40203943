using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    [SerializeField] private float range;
    [SerializeField] private float rotationSpeed;

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

    private bool IsInRange => Vector3.Distance(playerPosition.position, transform.position) <= range;

    private void TurnToPlayer()
    {
        var relativePosition = playerPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
