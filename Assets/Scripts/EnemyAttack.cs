using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public void Attack(int damageAmount)
    {
        PlayerCombat.currentHealth -= damageAmount;
    }
}
