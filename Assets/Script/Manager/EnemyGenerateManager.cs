using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class EnemyGenerateManager : MonoBehaviour
{
    [SerializeField] Transform baseTransform;
    [SerializeField] Transform[] generatePoint;
    [SerializeField] GameObject enemy;

    private float generateSpan = 2.0f;
    private void Start()
    {
        GenerateObservable();   
    }
    private void RandomGenerateEnemy()
    {
        var number = Random.Range(0, generatePoint.Length);

        enemy.GetComponent<TestEnemyController>().target = baseTransform;
        Instantiate(enemy, generatePoint[number].position, Quaternion.identity);

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
