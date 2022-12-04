using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public Slider PlayerHealth;
    public GameObject GameOverPanel;
    public TMP_Text SurvivalTimeText;
    public TMP_Text MaxSurvivalTimeText;

    private PlayerController scriptPC;

    private float maxTime;

    void Start()
    {
        Time.timeScale = 1;
        scriptPC = GameObject.FindWithTag(Tags.PLAYER)
            .GetComponent<PlayerController>();
        PlayerHealth.maxValue = scriptPC.status.Health;
        UpdateHealth();
        maxTime = PlayerPrefs.GetFloat("MaxScore");
    }

    public void UpdateHealth()
    {
        PlayerHealth.value = scriptPC.status.Health;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);

        int minutes = (int) (Time.timeSinceLevelLoad / 60);
        int seconds = (int) (Time.timeSinceLevelLoad % 60);

        SurvivalTimeText.text = string.Format("Você sobreviveu por {0}min e {1}s!", minutes, seconds);

        UpdateMaxScore(minutes, seconds);
    }

    public void UpdateMaxScore(int minutes, int seconds)
    {
        if (Time.timeSinceLevelLoad > maxTime)
        {
            maxTime = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("MaxScore", maxTime);
        }
        else
        {
            minutes = (int) (maxTime / 60);
            seconds = (int) (maxTime % 60);
        }

        MaxSurvivalTimeText.text = string.Format("Melhor tempo: {0}min e {1}s.", minutes, seconds);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
