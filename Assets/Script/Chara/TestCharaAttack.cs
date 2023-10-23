using UnityEngine;
using Enemy;

public class TestCharaAttack : MonoBehaviour,ICharaAttack
{

    public void Attack(GameObject enemy,float attackPower) 
    {
          var enemyHp = enemy.GetComponent<IEnemyHp>();
          enemyHp.DecreaseHp(attackPower);

    }
}
