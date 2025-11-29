using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerDamager : MonoBehaviour
{
    public DaggerWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,weapon.durationTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Enemy")
       {
            collision.GetComponent<EnemyController>().TakeDamage(weapon.damage, weapon.shouldKonckBack);
            Object.Destroy(gameObject);
       }
    }
}
