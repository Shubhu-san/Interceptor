using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    void Awake(){
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame(){
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameOver(){
        SceneManager.LoadScene("GameOver");
    }   

    public void QuitGame(){
        Application.Quit();
    }

}
