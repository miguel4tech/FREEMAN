using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    
    float next_spawn_time;

    void Start()
    {
        //start off with next spawn time being 'in 5 seconds'
        next_spawn_time = Time.time+5.0f;

    }
    void Update()
    {
        if(Time.time > next_spawn_time)
        {
            EnemyWave();
            
            //increment next_spawn_time
            next_spawn_time += 5.0f;
        }
    
    }

    void EnemyWave()
    {
        float spawnPosition = Random.Range(-10f, 5f);
        var newEnemy = Instantiate(enemy, new Vector3(spawnPosition, 0.95f, 0), Quaternion.Euler(0,-90,0));
        newEnemy.transform.parent = gameObject.transform;
    }
}