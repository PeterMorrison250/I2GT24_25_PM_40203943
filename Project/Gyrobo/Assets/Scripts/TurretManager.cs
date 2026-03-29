using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public Rigidbody projectilePrefab;
    
    [SerializeField] private float range;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private int fireForce;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform projectileSpawnPointPosition;

    private float _nextFire;
    private LayerMask _layerMask;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Player");
    }
    
    void FixedUpdate()
    {
        if (!IsInRange)
        {
            return;
        }
        
        TrackPlayer();
    }

    private bool IsInRange => Vector3.Distance(playerPosition.position, transform.position) <= range;

    private void TurnToPlayer()
    {
        var relativePosition = playerPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void TrackPlayer()
    {
        var moveDirection = (projectileSpawnPointPosition.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, moveDirection, out var hit, range, _layerMask))
        {
            Debug.DrawRay(transform.position, moveDirection * hit.distance, Color.yellow);
            FireProjectile();
        }
        else
        {
            Debug.DrawRay(transform.position, moveDirection * 1000, Color.white);
        }
        
        TurnToPlayer();
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
