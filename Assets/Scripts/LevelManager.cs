using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }
    private bool gameActive;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.Instance.UpdateTimer(timer);
        }
    }
    public void EndLevel()
    {
        //StartCoroutine(EndlevelCo());
        StartCoroutine(WaitAndPauseTime());
        
    }
    IEnumerator WaitAndPauseTime()
    {
        yield return new WaitForSeconds(1f); // µÈ´ý1Ãë
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        UIController.Instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        UIController.Instance.LevelEndScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    //IEnumerator EndlevelCo()
    //{
    //    yield return new WaitForSeconds(1f);
    //    float minutes = Mathf.FloorToInt(timer / 60);
    //    float seconds = Mathf.FloorToInt(timer % 60);
    //    UIController.Instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
    //    UIController.Instance.LevelEndScreen.SetActive(true);
    //}
}
