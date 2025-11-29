using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class DaggerWeapon : Weapon
{
    public GameObject dagger;
    private float timecount;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timecount -= Time.deltaTime;
        if (timecount <= 0)
        {
            timecount=timeBetweenAttacks;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range,whatIsEnemy);
            if(enemies.Length > 0)
            {
                for(int i = 0; i <amount; i++)
                {
                    Vector3 targetPos = enemies[Random.Range(0,enemies.Length)].transform.position;
                    Vector3 direction=targetPos - transform.position;
                    float angle=Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
                    angle -= 90;
                    Transform a=transform;
                    a.transform.rotation=Quaternion.AngleAxis(angle,Vector3.forward);
                    GameObject child = Instantiate(dagger, transform.position, a.transform.rotation);
                    child.GetComponent<DaggerDamager>().weapon = this;

                }
                SFXManager.instance.PlaySFXitched(6);
            }
        }
    }
}
