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
    
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
        _playerController.HasDied += HandlePlayerHasDied;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > Constants.Level1LeftBoundary
            || player.transform.position.x < Constants.Level1RightBoundary
            || player.transform.position.y > Constants.Level1TopBoundary
            || player.transform.position.y < Constants.Level1BottomBoundary)
        {
            _playerController.DamagePlayer();
        }
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
