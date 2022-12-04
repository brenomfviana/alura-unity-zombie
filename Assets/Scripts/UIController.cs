using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public Slider PlayerHealth;
    public GameObject GameOverPanel;
    public TMP_Text SurvivalTimeText;

    private PlayerController scriptPC;

    private static string MESSAGE = "Você sobreviveu por {}min e {}s!";

    void Start()
    {
        Time.timeScale = 1;
        scriptPC = GameObject.FindWithTag(Tags.PLAYER)
            .GetComponent<PlayerController>();
        PlayerHealth.maxValue = scriptPC.status.Health;
        UpdateHealth();
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

        Regex regex = new Regex("{}");
        string message = regex.Replace(MESSAGE, minutes.ToString(), 1);
        message = regex.Replace(message, seconds.ToString(), 1);

        SurvivalTimeText.text = message;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
