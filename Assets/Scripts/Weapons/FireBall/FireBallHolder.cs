using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBallHolder : MonoBehaviour
{
    public FireBallWeapon fireWeapon;
    public Object fireBallToSpawn;
    private float spawnCounter;
    public float timeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn=fireWeapon.timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + (fireWeapon.speed * Time.deltaTime));
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;
            float angleStep = 360f / fireWeapon.amount;

            for (int i = 0; i < fireWeapon.amount; i++)
            {
                // 当前火球的角度 = holder的旋转角度 + 间隔角度 * 索引（转为弧度）
                float currentAngle = (transform.eulerAngles.z + angleStep * i) * Mathf.Deg2Rad;
                // 计算圆周上的位置
                Vector3 spawnPos = transform.position + new Vector3(
                    Mathf.Cos(currentAngle),
                    Mathf.Sin(currentAngle),
                    0f
                ) * fireWeapon.spawnRadius;
                GameObject newFireBall = (GameObject)Instantiate(fireBallToSpawn, spawnPos, Quaternion.identity, transform);
                newFireBall.SetActive(true); // 激活物体
            }
            SFXManager.instance.PlaySFXitched(8);

        }
    }
}
