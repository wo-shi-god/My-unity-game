using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    public GameObject axeToSpawn;
    private float thorwCounter;
    // Start is called before the first frame update
    void Start()
    {
        thorwCounter = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        thorwCounter-=Time.deltaTime;
        if(thorwCounter < 0)
        {
            thorwCounter = timeBetweenAttacks;
            for (int i = 0; i < amount; i++)
            {
                GameObject a = Instantiate(axeToSpawn, transform.position, Quaternion.identity);
                a.GetComponent<AxeDamager>().weapon = this;
            }
            SFXManager.instance.PlaySFXitched(4);
        }
    }
}
