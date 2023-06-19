using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Enemy.Test;

public class EnemyGenerateManager : MonoBehaviour
{
    [SerializeField] Transform baseTransform;//Serialize‚ğÁ‚·‚ÆƒoƒO‚é
    [SerializeField] Transform[] generatePoint;
    [SerializeField] GameObject[] enemy;

    private float generateSpan = 2.0f;
    private void Start()
    {
        GenerateObservable();   
    }
    private void RandomGenerateEnemy()
    {
        var number = Random.Range(0, generatePoint.Length);

        var enemyNum = Random.Range(0, enemy.Length);

        enemy[enemyNum].GetComponent<TestEnemyController>().target = baseTransform;
        if (enemy[enemyNum].GetComponent<TestEnemyController>().target == null) Debug.Log("nullnull");
        Instantiate(enemy[enemyNum], generatePoint[number].position, Quaternion.identity);

    }
    private void GenerateObservable() 
    {
        this.UpdateAsObservable()
            .ThrottleFirst(System.TimeSpan.FromSeconds(generateSpan))
            .Subscribe(_ =>
            {
                RandomGenerateEnemy();
            })
            .AddTo(this);
    }
    
}
