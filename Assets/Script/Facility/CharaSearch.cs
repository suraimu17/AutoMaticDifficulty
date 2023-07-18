using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSearch : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>();

    private GameObject targetEnemy;

    public GameObject FindNearerstEnemy() 
    {
        if (enemyList == null) return null;

        foreach (GameObject enemy in enemyList) 
        {
            var distance = Vector3.Distance(this.transform.position, enemy.transform.position);
            //Debug.Log(enemy);
            if (targetEnemy == null) return null;
            if (distance < Vector3.Distance(this.transform.position, targetEnemy.transform.position))
            {

                targetEnemy = enemy;
            }


        }

        return targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !enemyList.Contains(collision.gameObject))
        {

            enemyList.Add(collision.gameObject);
            //‰¼’u‚«
            targetEnemy = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && enemyList.Contains(collision.gameObject))
        {
            enemyList.Remove(collision.gameObject);
        }
    }
}
