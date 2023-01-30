using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool GameEnded;

    public bool IsGameActive { get; set; }

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private GameObject gameoverPanel;

    private int score;

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void GameOver()
    {
        IsGameActive = false;
        GameEnded = true;

        StartCoroutine(WaitForExplosion());
    }

    private IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(0.8f);
        gameoverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        gameoverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
    }

    public void StartGame()
    {
        GameEnded = false;
        startButton.gameObject.SetActive(false);
        IsGameActive = true;
        score = 0;

        UpdateScore(0);
    }
}
