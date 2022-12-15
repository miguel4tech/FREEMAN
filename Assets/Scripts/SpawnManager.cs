using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int enemyCounts;
    private GameObject player;
    public bool spawnComplete;
    public int levelSpawnTimes;
    public int startSpawn;
    public int spawnCount;


    private void Start()
    {
        player = GameObject.Find("SwordWarrior");
        spawnComplete = false;
        startSpawn = 2;
        spawnCount = 0;
    }
  
    public void SpawnEnemies()
    {
        enemyCounts = FindObjectsOfType<Enemy>().Length;
        if(enemyCounts != 0)
        {
            return;
        }
        else
        {
            if(spawnCount < levelSpawnTimes && spawnComplete == false)
            {
                for (int i = 0; i < startSpawn; i++)
                {
                    Vector3 enemyRange = new Vector3(Random.Range(3, 10), 1, 0);

                    Instantiate(enemyPrefab, player.transform.position + enemyRange, enemyPrefab.transform.rotation);

                }
                startSpawn += 2;
                spawnCount++;
                spawnComplete = false;
            }
            else
            {
                spawnComplete = true;
            }
          

        }

    }

}