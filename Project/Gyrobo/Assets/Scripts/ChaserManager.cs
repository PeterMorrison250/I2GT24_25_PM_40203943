using System;
using Gyrobo;
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
    [SerializeField] private Transform baseTrackerTransform;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody chaserRigidbody;
    [SerializeField] private PlayerController playerController;
    
    private LayerMask _playerLayerMask;
    private LayerMask _surfaceLayerMask;
    private ChaserState _chaserState;
    private ChaserTrackerDirection _lastTrackerDirection;
    private FacingDirection _facingDirection = FacingDirection.Left;
    private bool _isJumping;
    private bool _isAttacking;
    private GravityDirection _currentGravityDirection;

    private static readonly float GapDetectionRange = 5;
    private static readonly float ChaserDownPositionRange = 2;
    private static readonly float ChaserFrontPositionRange = 2;
    private static readonly float ChaserUpPositionRange = 10;
    
    private void Awake()
    {
        _playerLayerMask = LayerMask.GetMask(Constants.Tags.Player);
        _surfaceLayerMask = LayerMask.GetMask(Constants.Tags.Surface);
        _chaserState = ChaserState.Idle;
    }

    private void Start()
    {
        GravityController.GravityChanged += HandleGravityChanged;
    }

    private void OnDisable()
    {
        GravityController.GravityChanged -= HandleGravityChanged;
    }
    
    private void FixedUpdate()
    {
        if (!IsInChasingRange)
        {
            _lastTrackerDirection = ChaserTrackerDirection.None;
            _chaserState = ChaserState.Idle;
            return;
        }

        TrackPlayer();
        TrackSurfaces();
    }

    protected void OnCollisionEnter(Collision other)
    {
        HandleLanding(other);

        if (_isAttacking && other.gameObject.CompareTag(Constants.Tags.Player))
        {
            playerController.DamagePlayer();
        }
    }

    private bool IsInChasingRange => Vector3.Distance(playerTransform.position, transform.position) <= chasingRange;

    private bool IsInAttackingRange()
    {
        _isAttacking = Vector3.Distance(playerTransform.position, transform.position) <= attackingRange;
        return _isAttacking;
    }

    private bool HasPlayerJumpedBehindChaser => _lastTrackerDirection is not ChaserTrackerDirection.None
                                                && TrackRaycast(backTrackerTransform, _playerLayerMask, out _);

    private void TrackPlayer()
    {
        if (DetectIfPlayerIsInFront())
        {
            ChasePlayer();
        }

        if (HasPlayerJumpedBehindChaser)
        {
            ChangeDirection();
        }
    }

    private bool DetectIfPlayerIsInFront()
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
        
        return isTracking;
    }

    private void TrackSurfaces()
    {
        if (!_isJumping)
        {
            TrackGaps();
            TrackLedges();
        }
    }

    private void TrackGaps()
    {
        if (TrackRaycast(downTrackerTransform, _surfaceLayerMask, out var hit, GapDetectionRange))
        { 
            if (hit is null)
            { 
                return;
            }

            if (TrackRaycast(baseTrackerTransform, _surfaceLayerMask, out var baseHit, ChaserDownPositionRange))
            {
                if (baseHit is null)
                { 
                    return;
                }
                    
                var heightDifferenceBetweenChaserAndFloor =
                        GravityDirectionHandler.GetFloorPointValueDependentOnGravity(_currentGravityDirection,
                            baseHit.Value.point)
                        - GravityDirectionHandler.GetFloorPointValueDependentOnGravity(_currentGravityDirection,
                            hit.Value.point);

                var isThereGapInFrontOfChaser = Math.Abs(heightDifferenceBetweenChaserAndFloor) > 0.00001;
                if (isThereGapInFrontOfChaser)
                {
                    Jump();
                }
            }
        }
    }

    private void TrackLedges()
    {
        if (TrackRaycast(frontTrackerTransform, _surfaceLayerMask, out var frontHit, ChaserFrontPositionRange))
        {
            if (frontHit is null)
            {
                return;
            }

            if (TrackRaycast(upTrackerTransform, _surfaceLayerMask, out var _, ChaserUpPositionRange))
            {
                return;
            }
            
            Jump();
        }
    }

    private void Jump()
    {
        if (_isJumping)
        {
            return;
        }
        
        _isJumping = true;
        chaserRigidbody.AddRelativeForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private bool TrackRaycast(Transform trackerTransform, LayerMask layerMask, out RaycastHit? hitInfo, float range = 15)
    {
        var moveDirection = (trackerTransform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, moveDirection, out var hit, range, layerMask))
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
            var positionAlongPlatform = GravityDirectionHandler.TrackAlongSurface(_currentGravityDirection, transform, playerTransform);
            transform.position = Vector3.MoveTowards(transform.position, positionAlongPlatform, step);
        }

        _chaserState = IsInAttackingRange() ? ChaserState.Attacking : ChaserState.Chasing;
    }
    
    private void ChangeDirection()
    {
        switch (_facingDirection)
        {
            case FacingDirection.Right:
            default:
                _facingDirection = FacingDirection.Left;
                transform.rotation = GravityDirectionHandler.Face(_currentGravityDirection, FacingDirection.Left);
                break;
            case FacingDirection.Left:
                _facingDirection = FacingDirection.Right;
                transform.rotation = GravityDirectionHandler.Face(_currentGravityDirection, FacingDirection.Right);
                break;
        }

        _lastTrackerDirection = ChaserTrackerDirection.None;
    }

    private void HandleLanding(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.Tags.Surface))
        {
            _isJumping = false;
        }
    }
    
    private void HandleGravityChanged(object sender, GravityChangedEventArgs e)
    {
        _currentGravityDirection = e.GravityDirection;
        RotateToGravity();
    }
    
    private void RotateToGravity()
    {
        transform.rotation = GravityDirectionHandler.Face(_currentGravityDirection, _facingDirection);
    }
}
