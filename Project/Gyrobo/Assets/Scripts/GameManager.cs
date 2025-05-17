using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    
    public TextMeshProUGUI gameOverText;
    
    public Button restartButton;
    
    public bool IsGameOver = false;
    
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
        GameOver();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        IsGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
