using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public float rotateSpeed;
    public Transform holder;
    public Object fireBallToSpawn;
    public float timeBetweenSpan;
    private float spawnCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=Quaternion.Euler(0f,0f,transform.rotation.eulerAngles.z+(rotateSpeed*Time.deltaTime));
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0 )
        {
            spawnCounter = timeBetweenSpan;
            Instantiate(fireBallToSpawn, holder.position, transform.rotation, holder);
        }
    }
}
