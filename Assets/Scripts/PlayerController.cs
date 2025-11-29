using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject fireBallholder;
    private void Awake()
    {
        instance = this;
    }


    public float speed;
    public Animator animator;


    public float pickupRange;
    //public Weapon activeWeapom;
    public List<Weapon> unassignedWeapons, assignedWeapons;
    public List<Weapon> MaxLevelWeapons;
    // Start is called before the first frame update
    void Start()
    {
        AddWeapon(unassignedWeapons[Random.Range(0,unassignedWeapons.Count)]);
    }

    // Update is called once per frame



    void Update()
    {
        //µ±«∞Œª÷√
        Vector3 moveInput= new Vector3(0f,0f,0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        transform.position += moveInput*speed*Time.deltaTime;
        if(moveInput!=Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    public void AddWeapon(int weaponNumber)
    {
        if (unassignedWeapons[weaponNumber].name == "FireBall")
        {
            fireBallholder.gameObject.SetActive(true);
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons.Remove(unassignedWeapons[weaponNumber]);
            return;
        }
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }
    public void AddWeapon(Weapon weapon)
    {
        if (weapon.name == "FireBall")
        {
            fireBallholder.gameObject.SetActive(true);
            assignedWeapons.Add(weapon);
            unassignedWeapons.Remove(weapon);
            return;
        }
        weapon.gameObject.SetActive(true) ;
        assignedWeapons.Add((Weapon)weapon);
        unassignedWeapons.Remove(weapon);
    }
}
