using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    private void Awake()
    {
        instance = this;
    }
    public DamageNumber numberToSpawn;
    public Transform numberCanvas;
    private List<DamageNumber> numberPool = new List<DamageNumber>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SpawnDamage(float damage,Vector3 location)
    {
        int rounded=Mathf.RoundToInt(damage);
        //DamageNumber newDamage=   Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        DamageNumber newDamage = GetFromPool();//设置数字池，减少创建和销毁所用的消耗
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;
    }
    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutPut = null;
        if (numberPool.Count == 0)
        {
            numberToOutPut = Instantiate(numberToSpawn, numberCanvas);
        }
        else
        {
            numberToOutPut = numberPool[0];
            numberPool.RemoveAt(0);
        }


        return numberToOutPut;
    }
    public void PlaceInPool(DamageNumber number)
    {
        number.gameObject.SetActive(false);
        numberPool.Add(number);

    }
}
