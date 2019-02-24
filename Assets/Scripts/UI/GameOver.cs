using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text playerName;
    public void Retry()
    {
        if (!string.IsNullOrEmpty(playerName.text))
        {
            HighScore.AddScore("C62A1190B91BC6D55417EDAFA8856C0BD9C93108", PlayerPrefs.GetInt("TimeInt"), playerName.text);
        }
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        if (!string.IsNullOrEmpty(playerName.text))
        {
            print(PlayerPrefs.GetInt("TimeInt"));
            return;
            HighScore.AddScore("C62A1190B91BC6D55417EDAFA8856C0BD9C93108", PlayerPrefs.GetInt("TimeInt"), playerName.text);
        }
        SceneManager.LoadScene(0);
    }
}
