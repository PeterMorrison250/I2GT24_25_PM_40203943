using System.Collections;
using System.Collections.Generic;
using Gyrobo;
using Gyrobo.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour, IResetable
{
    public GameObject player;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    
    public GameObject finishCube;
    public TextMeshProUGUI levelCompleteText;
    public Button NextLevelButton;
    
    public bool IsGameOver = false;
    public bool IsLevelComplete = false;
    
    public bool IsChangingGravity { get; set; }
    public GravityDirection GravityDirection { get; set; }
    
    private PlayerController _playerController;
    private GravityController _gravityController;
    private LevelController _levelController;
    private LevelBoundary _levelBoundary;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
        _gravityController = GetComponent<GravityController>();
        _playerController.HasDied += HandlePlayerHasDied;
        _levelController = finishCube.GetComponent<LevelController>();
        _levelController.LevelComplete += HandleLevelComplete;

        var sceneName = SceneManager.GetActiveScene().name;
        _levelBoundary = LevelBoundaryManager.LevelBoundaryDictionary[sceneName];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >  _levelBoundary.MinX
            || player.transform.position.x < _levelBoundary.MaxX
            || player.transform.position.y > _levelBoundary.MaxY
            || player.transform.position.y < _levelBoundary.MinY)
        {
            _playerController.DamagePlayer();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        IsGameOver = true;
    }

    public void LevelComplete()
    {
        IsLevelComplete = true;
        levelCompleteText.gameObject.SetActive(true);
        NextLevelButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Reset();
        _gravityController.Reset();
        _playerController.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Reset()
    {
        IsGameOver = false;
        IsChangingGravity = false;
        IsLevelComplete = false;
    }
    
    private void HandlePlayerHasDied(object sender, System.EventArgs e)
    {
        GameOver();
    }

    private void HandleLevelComplete(object sender, System.EventArgs e)
    {
        LevelComplete();
    }
}
