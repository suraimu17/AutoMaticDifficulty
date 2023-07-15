using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Manager
{
    public class EnemyPattern : MonoBehaviour
    {




        //delegateÅ@égÇ¶ÇÈÇ©Ç‡
        public async UniTaskVoid TankPattern(GameObject TankEnemy, GameObject Enemy, int num, Vector3 generatePos) //asyncÇ…Ç∑ÇÈ
        {
            
            Instantiate(TankEnemy, generatePos, Quaternion.identity);


            for (int i = 0; i < num - 1; i++)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));
                Instantiate(Enemy, generatePos, Quaternion.identity);
            }

        }

    }
}