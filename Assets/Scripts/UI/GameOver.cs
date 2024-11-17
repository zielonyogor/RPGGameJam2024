using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeScore(float score)
    {
        scoreText.text = TimeSpan.FromSeconds(score).ToString("mm':'ss'.'f");
    }
}
