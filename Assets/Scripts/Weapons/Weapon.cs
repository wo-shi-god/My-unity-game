using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int amount;
    public float speed, damage, range, timeBetweenAttacks, durationTime, spawnRadius;
    public int weaponLevel;
    public int MaxLevel;
    public bool shouldKonckBack;
    public Sprite icon;

    public GameObject carrierObject;

    // 枚举
    public enum StatType
    {
        Amount, Speed, Damage, Range, Cooldown, Duration, SpawnRadius
    }
    [Header("属性配置")]
    public List<StatType> supportedStats = new List<StatType>();
    public struct UpgradePreviewData
    {
        public StatType statType; // 升级的属性类型
        public float oldValue;    // 升级前数值
        public float newValue;    // 升级后数值
        public string desc;       // 描述文本
    }
    public UpgradePreviewData upgradePreviewData=new UpgradePreviewData();
    public UpgradePreviewData[] upgradeStore = new UpgradePreviewData[3];
    public void LevelUp(int num)
    {
        weaponLevel++;
        ApplyPreviewUpgrade(num);
        if (!PlayerController.instance.MaxLevelWeapons.Contains(this))
        {
            if (weaponLevel >= MaxLevel)
            {
                PlayerController.instance.MaxLevelWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
                weaponLevel = MaxLevel;
            }
        }
    }

    [Header("升级属性倍率")]
    public float amountUpgradeRate = 1.2f;
    public float speedUpgradeRate = 1.1f;
    public float damageUpgradeRate = 1.15f;
    public float rangeUpgradeRate = 1.2f;
    public float cooldownReduceRate = 0.9f; // 攻击间隔降低（乘以小于1的数）
    public float durationUpgradeRate = 1.2f;
    public float spawnRadiusUpgradeRate = 1.1f;
    public void UpgradePreview(int number)
    {
        upgradePreviewData = new UpgradePreviewData();
        if (supportedStats == null || supportedStats.Count == 0)
        {
            // 默认支持所有属性（兼容旧逻辑）
            supportedStats = System.Enum.GetValues(typeof(StatType)).Cast<StatType>().ToList();
        }

        // 从支持的属性中随机选择
        StatType randomStat = supportedStats[Random.Range(0, supportedStats.Count)];
        upgradePreviewData.statType = randomStat;
        string previewDesc = "";
        switch (randomStat)
        {
            case StatType.Amount:
                int oldAmount = amount;
                int newAmount = Mathf.RoundToInt(amount * amountUpgradeRate);
                upgradePreviewData.newValue = newAmount;
                upgradePreviewData.oldValue = oldAmount;
                previewDesc = $"amount +{newAmount - oldAmount}";
                upgradePreviewData.desc = previewDesc;
                break;

            case StatType.Speed:
                float oldSpeed = speed;
                float newSpeed = speed * speedUpgradeRate;
                upgradePreviewData.newValue = newSpeed;
                upgradePreviewData.oldValue = oldSpeed;
                previewDesc = $"speed +{Mathf.Round((newSpeed - oldSpeed) * 100) / 100}";
                upgradePreviewData.desc = previewDesc;
                break;

            case StatType.Damage:
                float oldDamage = damage;
                float newDamage = damage * damageUpgradeRate;
                upgradePreviewData.newValue = newDamage;
                upgradePreviewData.oldValue = oldDamage;
                previewDesc = $"damage +{Mathf.Round((newDamage - oldDamage) * 100) / 100}";
                upgradePreviewData.desc = previewDesc;
                break;

            case StatType.Range:
                float oldRange = range;
                float newRange = range * rangeUpgradeRate;
                upgradePreviewData.newValue = newRange;
                upgradePreviewData.oldValue = oldRange;
                previewDesc = $"range +{Mathf.Round((newRange - oldRange) * 100) / 100}";
                upgradePreviewData.desc = previewDesc;
                break;

            case StatType.Cooldown:
                float oldCooldown = timeBetweenAttacks;
                float newCooldown = timeBetweenAttacks * cooldownReduceRate;
                upgradePreviewData.newValue = newCooldown;
                upgradePreviewData.oldValue = oldCooldown;
                previewDesc = $"timeBetweenAttacks -{Mathf.Round((oldCooldown - newCooldown) * 100) / 100}";
                upgradePreviewData.desc = previewDesc;
                break;

            case StatType.Duration:
                float oldDuration = durationTime;
                float newDuration = durationTime * durationUpgradeRate;
                upgradePreviewData.newValue = newDuration;
                upgradePreviewData.oldValue = oldDuration;
                previewDesc = $"durationTime +{Mathf.Round((newDuration - oldDuration) * 100) / 100}";
                upgradePreviewData.desc = previewDesc;
                break;

            case StatType.SpawnRadius:
                float oldRadius = spawnRadius;
                float newRadius = spawnRadius * spawnRadiusUpgradeRate;
                upgradePreviewData.newValue = newRadius;
                upgradePreviewData.oldValue = oldRadius;
                previewDesc = $"spawnRadius +{Mathf.Round((newRadius - oldRadius) * 100) / 100}";
                upgradePreviewData.desc = previewDesc;
                break;
        }
        upgradeStore[number] = upgradePreviewData;
        Debug.Log(upgradePreviewData.desc);
        Debug.Log(upgradePreviewData.statType);
    }
    private void ApplyPreviewUpgrade(int num)
    {
        if (upgradePreviewData.statType == default(StatType) && upgradePreviewData.newValue == 0)
            return;
        switch (upgradeStore[num].statType)
        {
            case StatType.Amount:
                amount = Mathf.RoundToInt(upgradeStore[num].newValue);
                break;
            case StatType.Speed:
                speed = upgradeStore[num].newValue;
                break;
            case StatType.Damage:
                damage = upgradeStore[num].newValue;
                break;
            case StatType.Range:
                range = upgradeStore[num].newValue;
                break;
            case StatType.Cooldown:
                timeBetweenAttacks = upgradeStore[num].newValue;
                break;
            case StatType.Duration:
                durationTime = upgradeStore[num].newValue;
                break;
            case StatType.SpawnRadius:
                spawnRadius = upgradeStore[num].newValue;
                break;
        }

        // 清空预览数据，避免重复应用
        upgradePreviewData = new UpgradePreviewData();
        upgradeStore[num]=upgradePreviewData;
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