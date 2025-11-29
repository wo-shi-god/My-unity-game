using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
}
