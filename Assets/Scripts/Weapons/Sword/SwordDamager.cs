using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamager : MonoBehaviour
{
    public CloseAttackWeapon weapon;
    private Vector3 targetSize;
    private float growSpeed = 0.2f;
    private float growcount=1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.5f);
        transform.localScale = Vector3.one * weapon.range;
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        growcount-=Time.deltaTime;
        if (transform.localScale == targetSize&& growcount<=0.3f)
        {
            targetSize = Vector3.zero;
        }
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(weapon.damage, weapon.shouldKonckBack);
        }
    }
}
