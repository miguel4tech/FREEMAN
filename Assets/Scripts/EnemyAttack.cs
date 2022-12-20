using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerCombat playerCombat;
    void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
    }
    public void Attack(int damageAmount)
    {
        PlayerCombat.currentHealth -= damageAmount;
    }
}
