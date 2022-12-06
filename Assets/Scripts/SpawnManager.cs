using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    void OnTriggerEnter()
    {
        var newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        newEnemy.transform.parent = gameObject.transform;
        //Object.Destroy(gameObject, 20f);
    }
}