using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class TestFacilityAttack : MonoBehaviour
{
    
    public void Attack(GameObject enemy,int attackPower) 
    {
        var enemyHp = enemy.GetComponent<IEnemyHp>();
        enemyHp.DecreaseHp(attackPower);
    }
}
