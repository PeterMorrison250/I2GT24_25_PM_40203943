using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    
    public TextMeshProUGUI gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.HasDied += HandlePlayerHasDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandlePlayerHasDied(object sender, System.EventArgs e)
    {
        gameOverText.gameObject.SetActive(true);
    }
}
