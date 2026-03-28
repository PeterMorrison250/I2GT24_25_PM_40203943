using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Surface"))
        {
            Destroy(gameObject);
        }
    }
}
