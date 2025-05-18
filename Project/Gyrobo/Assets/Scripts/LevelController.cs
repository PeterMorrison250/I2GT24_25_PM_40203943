using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public event EventHandler LevelComplete;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelComplete()
    {
        LevelComplete?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnLevelComplete();
    }
}
