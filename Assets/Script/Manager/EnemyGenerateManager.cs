using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Enemy.Test;

namespace Manager
{
    public class EnemyGenerateManager : MonoBehaviour
    {
        [SerializeField] Transform baseTransform;//SerializeÇè¡Ç∑Ç∆ÉoÉOÇÈ
        [SerializeField] Transform[] generatePoint;
        [SerializeField] GameObject[] enemy;

        private float generateSpan = 3.0f;
        private void Start()
        {
            for (int i = 0; i < enemy.Length; i++) 
            {
                enemy[i].GetComponent<TestEnemyController>().target = baseTransform;
            }

            GenerateObservable();
        }
        private void RandomGenerateEnemy()
        {
            var spawnNum = Random.Range(0, generatePoint.Length);
            var enemyNum = Random.Range(0, enemy.Length);
            //Debug.Log("enemynum"+enemyNum);

            Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);

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
}
