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
    [SerializeField] private Transform backTrackerTransform;
    [SerializeField] private float speed;
    
    private LayerMask _playerLayerMask;
    private LayerMask _surfaceLayerMask;
    private ChaserState _chaserState;
    private ChaserTrackerDirection _lastTrackerDirection;
    private FacingDirection _facingDirection = FacingDirection.Left;
    
    private void Awake()
    {
        _playerLayerMask = LayerMask.GetMask("Player");
        _surfaceLayerMask = LayerMask.GetMask("Surface");
        _chaserState = ChaserState.Idle;
    }
    
    private void FixedUpdate()
    {
        if (!IsInChasingRange)
        {
            _lastTrackerDirection = ChaserTrackerDirection.None;
            return;
        }

        TrackPlayer();
        TrackSurfaces();
    }
    
    private bool IsInChasingRange => Vector3.Distance(playerTransform.position, transform.position) <= chasingRange;
    private bool IsInAttackingRange => Vector3.Distance(playerTransform.position, transform.position) <= attackingRange;

    private void TrackPlayer()
    {
        var isTracking = false;
        
        if (TrackRaycast(frontTrackerTransform, _playerLayerMask, out _))
        {
            _lastTrackerDirection = ChaserTrackerDirection.Front;
            isTracking = true;
        }
        
        if (TrackRaycast(upTrackerTransform, _playerLayerMask, out _))
        {
            _lastTrackerDirection = ChaserTrackerDirection.Up;
            isTracking = true;
        }
        
        if (TrackRaycast(downTrackerTransform, _playerLayerMask, out _))
        {
            _lastTrackerDirection = ChaserTrackerDirection.Down;
            isTracking = true;
        }

        if (isTracking)
        {
            ChasePlayer();
        }

        if (_lastTrackerDirection is not ChaserTrackerDirection.None
            && TrackRaycast(backTrackerTransform, _playerLayerMask, out _))
        {
            ChangeDirection();
        }
    }

    private void TrackSurfaces()
    {
        if (TrackRaycast(downTrackerTransform, _surfaceLayerMask, out var hit))
        {
            if (hit is null)
            {
                return;
            }

            if (hit.Value.point.y < transform.position.y - 1)
            {
                var s = 5;
            }
        }
    }

    private bool TrackRaycast(Transform trackerTransform, LayerMask layerMask, out RaycastHit? hitInfo)
    {
        var moveDirection = (trackerTransform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, moveDirection, out var hit, chasingRange, layerMask))
        {
            Debug.DrawRay(transform.position, moveDirection * hit.distance, Color.yellow);
            hitInfo = hit;
            return true;
        }
        
        Debug.DrawRay(transform.position, moveDirection * 1000, Color.white);
        hitInfo = null;
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

    private void ChangeDirection()
    {
        if (_facingDirection == FacingDirection.Right)
        {
            _facingDirection = FacingDirection.Left;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (_facingDirection == FacingDirection.Left)
        {
            _facingDirection = FacingDirection.Right;
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        
        _lastTrackerDirection = ChaserTrackerDirection.None;
    }
}
