using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DaggerWeapon weapon;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * weapon.speed * Time.deltaTime;
    }
}
