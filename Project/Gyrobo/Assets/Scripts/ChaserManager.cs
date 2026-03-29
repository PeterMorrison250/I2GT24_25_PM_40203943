using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserManager : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform frontTrackerTransform;
    [SerializeField] private Transform upTrackerTransform;
    [SerializeField] private float speed;
    
    private LayerMask _layerMask;
    
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Player");
    }
    
    private void FixedUpdate()
    {
        if (!IsInRange)
        {
            return;
        }

        TrackPlayer();
    }
    
    private bool IsInRange => Vector3.Distance(playerTransform.position, transform.position) <= range;

    private void TrackPlayer()
    {
        var isTrackingFront = TrackRaycast(frontTrackerTransform);
        var isTrackingUp = TrackRaycast(upTrackerTransform);

        if (isTrackingFront
            || isTrackingUp)
        {
            ChasePlayer();
        }
    }

    private bool TrackRaycast(Transform trackerTransform)
    {
        var moveDirection = (trackerTransform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, moveDirection, out var hit, range, _layerMask))
        {
            Debug.DrawRay(transform.position, moveDirection * hit.distance, Color.yellow);
            return true;
        }
        
        Debug.DrawRay(transform.position, moveDirection * 1000, Color.white);
        return false;
    }

    private void ChasePlayer()
    {
        var step = speed * Time.deltaTime;
        var positionAlongPlatform = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, positionAlongPlatform, step);
    }
}
