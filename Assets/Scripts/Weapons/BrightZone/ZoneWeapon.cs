using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public bool damageOverTime;
    private float damageCounter;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * range;
        if (damageOverTime)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                damageCounter = timeBetweenAttacks;
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damage, shouldKonckBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
