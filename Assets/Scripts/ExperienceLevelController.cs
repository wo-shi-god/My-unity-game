using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    private void Awake()
    {
        instance = this;
    }
    public int currentExperience;
    public ExpPickUp pickUp;
    public List<int> expLevels;
    public int currentLevel,levelCount=50;
    // Start is called before the first frame update
    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count-1]*1.1f));
        }
        expLevels.Add(1000000000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetExp(int level)
    {
        currentExperience += level;
        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        if (currentLevel >= levelCount)
        {
            UIController.Instance.UpdateExp(currentExperience, expLevels[currentLevel], 1000000000);
        }
        else
        {
            UIController.Instance.UpdateExp(currentExperience, expLevels[currentLevel], currentLevel);
        }
        SFXManager.instance.PlaySFXitched(2);
    }    
    public void SpawnExp(Vector3 position,int ExpValue)
    {
        Instantiate(pickUp,position,Quaternion.identity).expValue=ExpValue;//Quaternion.identity是四元数无旋转角度
    }
    public void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];
        currentLevel++;

        // 生成三个不重复的按钮索引（简化逻辑）
        int num0 = Random.Range(0, 3);
        int num1 = (num0 + Random.Range(1, 3)) % 3; // 确保≠num0
        int num2 = 3 - num0 - num1;
        // 1. 初始化候选池：未分配武器 + 可升级的已分配武器（排除满级）
        List<Weapon> unassignedPool = new List<Weapon>(PlayerController.instance.unassignedWeapons ?? new List<Weapon>());
        List<Weapon> upgradablePool = PlayerController.instance.assignedWeapons
            .Where(w =>!PlayerController.instance.MaxLevelWeapons.Contains(w))
            .ToList();

        // 2. 确定新武器数量（保持原有概率逻辑）
        int maxNewWeapons = Mathf.Min(unassignedPool.Count, 3);
        int newWeaponCount = maxNewWeapons > 0 ? Random.Range(0, maxNewWeapons + 1) : 0;
        UpdateUniqueLevelUpButton(num0, newWeaponCount > 2, unassignedPool, upgradablePool, PlayerController.instance, UIController.Instance);
        UpdateUniqueLevelUpButton(num1, newWeaponCount > 1, unassignedPool, upgradablePool, PlayerController.instance, UIController.Instance);
        UpdateUniqueLevelUpButton(num2, newWeaponCount > 0, unassignedPool, upgradablePool, PlayerController.instance, UIController.Instance);


        UIController.Instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }


    // 更新单个升级按钮显示
    private void UpdateUniqueLevelUpButton(
       int buttonIndex,
       bool tryUseUnassigned,
       List<Weapon> unassignedPool,
       List<Weapon> upgradablePool,
       PlayerController player,
       UIController ui)
    {
        if (buttonIndex < 0 || buttonIndex >= ui.levelUpButtons.Length) return;

        Weapon selectedWeapon = null;

        // 优先选未分配武器（如果还有且需要）
        if (tryUseUnassigned && unassignedPool.Count > 0)
        {
            int idx = Random.Range(0, unassignedPool.Count);
            selectedWeapon = unassignedPool[idx];
            unassignedPool.RemoveAt(idx); // 选完移除，避免重复
        }
        // 再选可升级的已分配武器
        else if (upgradablePool.Count > 0)
        {
            int idx = Random.Range(0, upgradablePool.Count);
            selectedWeapon = upgradablePool[idx];
            upgradablePool.RemoveAt(idx); // 选完移除，避免重复
        }
        // 兜底：无可用武器（比如全满级），选任意已分配武器（允许重复）
        else if (player.assignedWeapons.Count > 0)
        {
            selectedWeapon = player.assignedWeapons[Random.Range(0, player.assignedWeapons.Count)];
        }
        else if (player.MaxLevelWeapons.Count > 0)
        {
            selectedWeapon = player.MaxLevelWeapons[Random.Range(0, player.MaxLevelWeapons.Count)];
        }

        // 更新按钮显示
        if (selectedWeapon != null)
        {
            ui.levelUpButtons[buttonIndex].UpdateButtonDisplay(selectedWeapon,buttonIndex);
        }
        else
        {
            // 无武器可选时隐藏按钮
            ui.levelUpButtons[buttonIndex].gameObject.SetActive(false);
        }
    }
    //public void LevelUp()
    //{
    //    currentExperience-=expLevels[currentLevel];
    //    currentLevel++;
    //    int num0 = Random.Range(0, 3);
    //    int num1 = Random.Range(0, 3);
    //    int num2= Random.Range(0, 3);
    //    while (num0 == num1)
    //    {
    //        num1 = Random.Range(0, 3);
    //    }
    //    while (num2 == num1||num2==num0)
    //    {
    //        num2 = Random.Range(0, 3);
    //    }
    //    if (PlayerController.instance.unassignedWeapons.Count > 0)
    //    {
    //        int numberToSelect = Random.Range(0, 4);
    //        if (numberToSelect == 0)
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        }
    //        if (numberToSelect == 1&&PlayerController.instance.unassignedWeapons.Count>=1)
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[Random.Range(0, PlayerController.instance.unassignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        }else
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        }
    //        if (numberToSelect == 2 && PlayerController.instance.unassignedWeapons.Count >= 2)
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[Random.Range(0, PlayerController.instance.unassignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[Random.Range(0, PlayerController.instance.unassignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        }
    //        else
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        }
    //        if (numberToSelect == 3 && PlayerController.instance.unassignedWeapons.Count >= 3)
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[Random.Range(0, PlayerController.instance.unassignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[Random.Range(0, PlayerController.instance.unassignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[Random.Range(0, PlayerController.instance.unassignedWeapons.Count)]);
    //        }
    //        else
    //        {
    //            UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //            UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        }


    //    }
    //    else
    //    {
    //        UIController.Instance.levelUpButtons[num0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        UIController.Instance.levelUpButtons[num1].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //        UIController.Instance.levelUpButtons[num2].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[Random.Range(0, PlayerController.instance.assignedWeapons.Count)]);
    //    }
    //    UIController.Instance.levelUpPanel.SetActive(true);
    //    Time.timeScale = 0f;
    //    //IController.Instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.activeWeapom);
    //}
}
