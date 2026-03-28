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

    private Transform _playerPosition;
    [SerializeField]
    private Transform projectileSpawnPointPosition;
    
    void Start()
    {
        _playerPosition = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (!IsInRange)
        {
            return;
        }
        
        FireProjectile();
        TurnToPlayer();
        
    }

    private bool IsInRange => Vector3.Distance(_playerPosition.position, transform.position) <= range;

    private void TurnToPlayer()
    {
        var relativePosition = _playerPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void FireProjectile()
    {
       Instantiate(projectilePrefab, projectileSpawnPointPosition.position, Quaternion.identity);
    }
}
