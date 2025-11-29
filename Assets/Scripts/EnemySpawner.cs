using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float timeToSpawn;
    private float spawnCounter;
    public Transform minSpawn, maxSpawn;

    private float despawnstance;
    private List<GameObject> spawnedEnemies=new List<GameObject>();
    public int checkPerFrame;
    private int enemyToCheck;
    public List<WaveInfo> waves;
    private int currentWave;
    private float waveCounter;
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter=timeToSpawn;
        despawnstance = Vector3.Distance(transform.position,maxSpawn.position) + 4f;//勾股定理算两点距离
        currentWave = -1;
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        //spawnCounter-=Time.deltaTime;
        //if (spawnCounter <= 0)
        //{
        //    spawnCounter = timeToSpawn;
        //    GameObject newEnemy=Instantiate(enemyToSpawn,selectSpawnPiont(),transform.rotation);
        //    spawnedEnemies.Add(newEnemy);
        //}
        if(PlayerHealth.Instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter-=Time.deltaTime;
                if (waveCounter < 0)
                {
                    GoToNextWave();
                }
                spawnCounter-=Time.deltaTime;
                if(spawnCounter < 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, selectSpawnPiont(), Quaternion.identity);
                    spawnedEnemies.Add(newEnemy);
                }
            }
        }



        int checkTarget=enemyToCheck+checkPerFrame;
        while (enemyToCheck<checkTarget)//每帧检查n个，不用每帧全部检查一遍，减少性能消耗
        {
            if (enemyToCheck < spawnedEnemies.Count) 
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnstance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }

    }
    public Vector3 selectSpawnPiont()
    {
        Vector3 spawnPoint=Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            if (Random.Range(0f, 1f) > .5f) {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            } 
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    }
    public void GoToNextWave()
    {
        currentWave++;
        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }

}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns=1f;

}