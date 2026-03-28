using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private float turretRange = 10;

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
    }

    private bool IsInRange => Vector3.Distance(playerPosition.position, transform.position) <= turretRange;
}
