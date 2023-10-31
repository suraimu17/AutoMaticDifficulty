using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Manager
{
    public class EnemyPattern : MonoBehaviour
    {
        public enum GeneratePattern 
        {
            Tank,//�^���N

        }



        //delegate�@�g���邩��
        public async UniTaskVoid TankPattern(GameObject TankEnemy, GameObject Enemy, int num, Vector3 generatePos) //async�ɂ���
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