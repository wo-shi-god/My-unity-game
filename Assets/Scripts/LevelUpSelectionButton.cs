using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;
    private Weapon assignedWeapon;
    private int num;
    public void UpdateButtonDisplay(Weapon theWeapon,int number)
    {
        num=number;
        assignedWeapon = theWeapon;
        if (PlayerController.instance.MaxLevelWeapons.Contains(theWeapon))
        {
            upgradeDescText.text = theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name + " LEVEL:MAX";
            return; // 满级直接返回，跳过后续逻辑
        }

        if (PlayerController.instance.assignedWeapons.Contains(theWeapon))
        {
            theWeapon.UpgradePreview(number);
            string upgradeDesc = theWeapon.upgradePreviewData.desc;
            upgradeDescText.text =  upgradeDesc;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name + "-lvl:" + theWeapon.weaponLevel;
        }
        else if (theWeapon == null)
        {
            upgradeDescText.text = null;
            weaponIcon.sprite = null;
            nameLevelText.text = "null";
        }
        else
        {
            upgradeDescText.text = "Unlock:" + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name;
        }
    }
    public void SelectUpgrade()
    {
        if (assignedWeapon == null) return;
        if (PlayerController.instance.MaxLevelWeapons.Contains(assignedWeapon))
        {
            return;
        }
        if (PlayerController.instance.assignedWeapons.Contains(assignedWeapon))
        {
            assignedWeapon.LevelUp(num);
        }
        else
        {
            PlayerController.instance.AddWeapon(assignedWeapon);
        }
        UIController.Instance.levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
