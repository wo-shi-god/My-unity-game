using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseAttackWeapon: Weapon
{
    private float timecount;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timecount -= Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                sword.transform.rotation = Quaternion.identity;
            }
            else
            {
                sword.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
        }
        if (timecount <= 0)
        {
            timecount = timeBetweenAttacks;
            for (int i = 0; i < amount; i++)
            {
                float rot = (360f / amount) * i;
                GameObject a = Instantiate(sword, transform.position,Quaternion.Euler(0f,0f,sword.transform.rotation.eulerAngles.z+rot), transform);
                a.GetComponent<SwordDamager>().weapon = this;
            }
            SFXManager.instance.PlaySFXitched(9);
        }
    }
}
