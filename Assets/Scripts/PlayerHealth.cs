using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public static PlayerHealth Instance;//µ¥ÀýÄ£Ê½
    public Slider healthSlider;
    public GameObject deathEffect;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue=maxHealth;
        healthSlider.value=currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TakgDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            LevelManager.instance.EndLevel();
            Instantiate(deathEffect,transform.position,transform.rotation);
            SFXManager.instance.PlaySFX(3);
        }
        healthSlider.value = currentHealth;
    }
}
