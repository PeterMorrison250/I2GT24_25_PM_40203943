using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserManager : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform playerTransform;


    private void FixedUpdate()
    {
        if (!IsInRange)
        {
            return;
        }
    }
    
    private bool IsInRange => Vector3.Distance(playerTransform.position, transform.position) <= range;

}
