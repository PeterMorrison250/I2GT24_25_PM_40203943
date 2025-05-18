using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlateController : MonoBehaviour
{
    public GameObject laserEmitter;
    
    private LaserController _laserController;
    
    // Start is called before the first frame update
    void Start()
    {
        _laserController = laserEmitter.GetComponent<LaserController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _laserController.IsBeamEnabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        _laserController.IsBeamEnabled = true;
    }
}
