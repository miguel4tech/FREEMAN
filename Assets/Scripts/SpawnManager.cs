using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public bool hasSpawn;
    void OnTriggerEnter(Collider other)
    {
        Vector3 newPosition = new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z);
        if(!hasSpawn && other.CompareTag("Player"))
        {
        var newEnemy = Instantiate(enemy, newPosition, Quaternion.identity);
        newEnemy.transform.parent = gameObject.transform;
        hasSpawn = true;
        }
    }
}