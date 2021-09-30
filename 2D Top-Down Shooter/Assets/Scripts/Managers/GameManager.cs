using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     public static GameManager instance;

    
     public bool isGameOver;
     public bool isLevelCompleted;

     public bool isGameStarted;

     public event Action OnGameStart;
     
     private void Awake()
     {
          instance = this;
     }

     private void Update()
     {
         
     }

     public void OnRetryButtonPressed()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }

     public void PlayButton()
     {
          isGameStarted = true;
         
          UIManager.instance.mainMenuPanel.SetActive(false);
          UIManager.instance.inGameUIPanel.SetActive(true);
          OnGameStart?.Invoke();
     }
     
}