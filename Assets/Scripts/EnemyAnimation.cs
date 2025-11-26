using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite;
    public float speed;
    public float minsize,maxsize;
    private float activesize;
    // Start is called before the first frame update
    void Start()
    {
        activesize = maxsize;   
    }

    // Update is called once per frame
    void Update()
    {
        sprite.localScale = Vector3.MoveTowards(sprite.localScale,Vector3.one*activesize,speed*Time.deltaTime);
        if (sprite.localScale.x == activesize) {
            if (activesize == maxsize)
            {
                activesize = minsize;
            }
            else if (activesize == minsize)
            {
                activesize = maxsize;
            }

        }
    }
}
