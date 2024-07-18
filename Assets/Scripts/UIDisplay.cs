using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Score")]
    ScoreKeeper scoreKeeper;
    [SerializeField]TextMeshProUGUI scoreText;

    LevelManager levelManager;

    [Header("Heath")]
    [SerializeField] Color defaultHPColor;
    [SerializeField] Color hPColor1;
    [SerializeField] Color hPColor2;
    [SerializeField] Color hPColor3;
    [SerializeField] Color hPColor4;
    [SerializeField] Slider healthBar;
    [SerializeField] Image healthBarImage;
    Health health;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        health = FindObjectOfType<Health>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("0000000");
        healthBar.value = health.GetPlayerHP();
        if (healthBar.value <= 0)
        {
            StartCoroutine(OnPlayerDeath());
        }
    }

    // void SetFillColor(Color color)
    // {
    //     healthBarImage.color = color;
    // }

    // void CheckHp(){
    //     if (healthBar.value == 100){
    //         SetFillColor(defaultHPColor);
    //     } else if(healthBar.value < 100 && healthBar.value >= 75){
    //         SetFillColor(hPColor1);
    //     } else if(healthBar.value < 75 && healthBar.value >= 50){
    //         SetFillColor(hPColor2);
    //     } else if(healthBar.value < 50 && healthBar.value >= 30){
    //         SetFillColor(hPColor3);
    //     } else {
    //         SetFillColor(hPColor4);
    //     }
    // }

    IEnumerator OnPlayerDeath(){
        yield return new WaitForSecondsRealtime(1.2f);
        levelManager.LoadGameOver();
    }

}
