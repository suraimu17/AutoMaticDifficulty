using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class TestCharaAttack : MonoBehaviour
{
    
    public void Attack(GameObject enemy,float attackPower) 
    {
        var enemyHp = enemy.GetComponent<IEnemyHp>();
        enemyHp.DecreaseHp(attackPower);
    }
}
