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
        IsFacingPlayer();
        TurnToPlayer();
        FireProjectile();
    }

    private bool IsInRange => Vector3.Distance(playerPosition.position, transform.position) <= range;

    private void TurnToPlayer()
    {
        var relativePosition = playerPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void IsFacingPlayer()
    {
        RaycastHit hit;

        
        var moveDirection = (transform.position - projectileSpawnPointPosition.position).normalized;
        
        if (Physics.Raycast(transform.position, moveDirection, out hit, 20))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(moveDirection) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(moveDirection), Color.white);
        }
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
