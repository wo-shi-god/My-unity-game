using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBallWeapon : Weapon
{
    private float growSpeed=0.1f;
    private Vector3 targetSize;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * range;
        Destroy(gameObject, durationTime);
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
        bool isSelfActive = gameObject.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage, shouldKonckBack);
        }
    }
}