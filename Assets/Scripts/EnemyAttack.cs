using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerCombat playerCombat;
    void Awake()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
    }
    public void Attack(int damageAmount)
    {
        playerCombat.currentHealth -= damageAmount;
    }
}
