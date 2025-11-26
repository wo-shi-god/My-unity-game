using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damage;
    public float lifeTime,growSpeed;
    private Vector3 targetSize;
    public bool shouldKonckBack;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale,targetSize,growSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage,shouldKonckBack);
        }
    }

}
