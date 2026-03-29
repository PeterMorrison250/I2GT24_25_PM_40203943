using Gyrobo.Enums;
using UnityEngine;

public class ChaserManager : MonoBehaviour
{
    [SerializeField] private float chasingRange;
    [SerializeField] private float attackingRange;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform frontTrackerTransform;
    [SerializeField] private Transform upTrackerTransform;
    [SerializeField] private Transform downTrackerTransform;
    [SerializeField] private float speed;
    
    private LayerMask _layerMask;
    private ChaserState _chaserState;
    
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Player");
        _chaserState = ChaserState.Idle;
    }
    
    private void FixedUpdate()
    {
        if (!IsInChasingRange)
        {
            return;
        }

        TrackPlayer();
    }
    
    private bool IsInChasingRange => Vector3.Distance(playerTransform.position, transform.position) <= chasingRange;
    private bool IsInAttackingRange => Vector3.Distance(playerTransform.position, transform.position) <= attackingRange;

    private void TrackPlayer()
    {
        var isTrackingFront = TrackRaycast(frontTrackerTransform);
        var isTrackingUp = TrackRaycast(upTrackerTransform);
        var isTrackingDown = TrackRaycast(downTrackerTransform);

        if (isTrackingFront
            || isTrackingUp
            || isTrackingDown)
        {
            ChasePlayer();
        }
        else
        {
            _chaserState = ChaserState.Idle;
        }
    }

    private bool TrackRaycast(Transform trackerTransform)
    {
        var moveDirection = (trackerTransform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, moveDirection, out var hit, chasingRange, _layerMask))
        {
            Debug.DrawRay(transform.position, moveDirection * hit.distance, Color.yellow);
            return true;
        }
        
        Debug.DrawRay(transform.position, moveDirection * 1000, Color.white);
        return false;
    }

    private void ChasePlayer()
    {
        if (_chaserState == ChaserState.Chasing)
        {
            var step = speed * Time.deltaTime;
            var positionAlongPlatform = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, positionAlongPlatform, step);
        }

        _chaserState = IsInAttackingRange ? ChaserState.Attacking : ChaserState.Chasing;
    }
}
