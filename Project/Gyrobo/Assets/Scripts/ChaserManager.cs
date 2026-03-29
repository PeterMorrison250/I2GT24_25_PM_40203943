using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserManager : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform frontTrackerTransform;
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
        var moveDirection = (frontTrackerTransform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, moveDirection, out var hit, range, _layerMask))
        {
            Debug.DrawRay(transform.position, moveDirection * hit.distance, Color.yellow);
            ChasePlayer();
        }
        else
        {
            Debug.DrawRay(transform.position, moveDirection * 1000, Color.white);
        }
    }

    private void ChasePlayer()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
    }
}
