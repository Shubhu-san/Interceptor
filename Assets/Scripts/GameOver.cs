using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake(){
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start(){
        scoreText.text = "Score\n" + scoreKeeper.GetScore();
    }
}
