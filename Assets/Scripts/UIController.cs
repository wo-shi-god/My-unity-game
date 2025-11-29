using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public static UIController Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Slider explvlslider;
    public TMP_Text explvlText;
    public LevelUpSelectionButton[] levelUpButtons;
    public GameObject levelUpPanel;
    public TMP_Text timeText;
    public GameObject LevelEndScreen;
    public TMP_Text endTimeText;
    public string mainMenu;
    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void UpdateExp(int currentExp,int levelExp,int currentLvl)
    {
        explvlslider.maxValue = levelExp;
        explvlslider.value = currentExp;
        explvlText.text = "Level:" + currentLvl;
        if(currentLvl== 1000000000)
        {
            explvlText.text = "Level:MAX";
        }
    }
    public void SkipLevelUp()
    {
        ExperienceLevelController.instance.currentLevel -=1;
        UpdateExp(ExperienceLevelController.instance.currentExperience, ExperienceLevelController.instance.expLevels[ExperienceLevelController.instance.currentLevel], ExperienceLevelController.instance.currentLevel);
        levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void UpdateTimer(float time)
    {
       float minutes=Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = "Time\n" +minutes + ":" + seconds.ToString("00");
    }
    public void GoTOMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale=1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Pause()
    {
        if (pauseScreen.activeSelf==false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale=0.0f;
        }
        else 
        {
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false)
                Time.timeScale = 1.0f;
        }

    }
}
