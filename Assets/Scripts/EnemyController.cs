using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D orb;//物理引擎
    public float speed;//移动速度
    private Transform target;
    public float damage;
    public float hitWaitTime = 1f;
    private float hitCounter;//变量不赋值默认是0
    public float health;
    public float konckBackTime=0.5f;
    private float konckBackCounter;

    // Start is called before the first frame update
    void Start()
    {
        target=FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (konckBackCounter > 0)
        {
            konckBackCounter-= Time.deltaTime;
            if (speed > 0) 
            {
                speed = -speed * 2;            
            }
            if (konckBackCounter <= 0)
            {
                speed = Mathf.Abs(speed * .5f);
            }
        }
        orb.velocity = (target.position-transform.position).normalized*speed;//velocity是速度（m/s）
        if (hitCounter > 0f)
        {
            hitCounter-=Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&hitCounter<=0f)
        {
            PlayerHealth.Instance.TakgDamage(damage);
            hitCounter = hitWaitTime;
        }
    }
    public void TakeDamage(float damage)
    {
        health-=damage;

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
        DamageNumberController.instance.SpawnDamage(damage,transform.position);
    }
    public void TakeDamage(float damage,bool shouldKonckBack)
    {
        TakeDamage(damage);
        if (shouldKonckBack)
        {
            konckBackCounter = konckBackTime;
        }

    }
}
