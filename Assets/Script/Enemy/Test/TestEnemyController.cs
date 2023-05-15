using UnityEngine;
using Base;

public class TestEnemyController : MonoBehaviour, IEnemyController
{
    protected EnemyMover enemyMover;
    private BaseHP baseHP;
    private void Start()
    {
        enemyMover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        enemyMover.EnemyMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Base") 
        {
            Debug.Log("Base");
            baseHP = collision.GetComponent<BaseHP>();
            if (baseHP == null) return;

            baseHP.DecreaseHp();
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.tag == "Base")
        {
            baseHP = collision.GetComponent<BaseHP>();
            if (baseHP == null) return;

            baseHP.DecreaseHp();
            Destroy(this);
        }
    }*/
}
