using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickUp : MonoBehaviour
{
    public int expValue;
    public bool movingToPlayer;
    public float movingSpeed;
    public float timeBetweenChecks = .2f;
    private float checkCounter;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealth.Instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position,PlayerHealth.Instance.transform.position,movingSpeed*Time.deltaTime);

        }
        else
        {
            checkCounter-=Time.deltaTime;
            if(checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;
            }
            if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
            {
                movingToPlayer=true;
                movingSpeed += player.speed;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);
            Destroy(gameObject);
        }
    }
}
