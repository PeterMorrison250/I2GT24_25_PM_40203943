using System;
using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public event EventHandler LevelComplete;

    private void OnLevelComplete()
    {
        LevelComplete?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Constants.Tags.Player))
        {
            OnLevelComplete();
        }
    }
}
