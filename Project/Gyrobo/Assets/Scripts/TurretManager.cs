using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretManager : MonoBehaviour
{
    public Rigidbody projectilePrefab;
    
    [SerializeField] private float range;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private int fireForce;

    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private Transform projectileSpawnPointPosition;

    private float _nextFire;

    void FixedUpdate()
    {
        if (!IsInRange)
        {
            return;
        }
        
        FireProjectile();
        TurnToPlayer();
        
    }

    private bool IsInRange => Vector3.Distance(playerPosition.position, transform.position) <= range;

    private void TurnToPlayer()
    {
        var relativePosition = playerPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void FireProjectile()
    {
        if (Time.time > _nextFire)
        {
            var projectileRigidbody = Instantiate(projectilePrefab, projectileSpawnPointPosition.position, Quaternion.identity);
            projectileRigidbody.AddForce(transform.forward * fireForce);
            _nextFire = Time.time + fireRate;
        }
    }
}
