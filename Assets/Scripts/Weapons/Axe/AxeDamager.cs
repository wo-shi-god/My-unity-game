using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamager : MonoBehaviour
{
    public AxeWeapon weapon;
    public float throwPower;
    public Rigidbody2D theAxe;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        transform.localScale = Vector3.one * weapon.range;
        theAxe.velocity=new Vector2(Random.Range(-throwPower,throwPower),throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f,0f,transform.rotation.eulerAngles.z+(rotateSpeed*360f*Time.deltaTime*Mathf.Sign(theAxe.velocity.x)));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeDamage(weapon.damage, weapon.shouldKonckBack);
        }
    }
}
