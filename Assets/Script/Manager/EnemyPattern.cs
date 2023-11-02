using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Manager
{
    public class EnemyPattern : MonoBehaviour
    {
        /*
        public enum GeneratePattern 
        {
            Tank,//ƒ^ƒ“ƒN
            All,//‘S•ûˆÊ
            CharaInter,//ˆê‚©Š‚É‚¢‚Á‚Ï‚¢

        }*/

       /* public void GetPettern(int Pattern,GameObject[] enemyList,Transform[] generatePosList) 
        {
            switch (Pattern) {

                case  0://Tank
                    TankPattern(enemyList[1], enemyList[0], generatePos);
                    break;
                case 1:
                    //ˆ—
                    break;
                case 2:
                    //ˆ—
                    break;
                case 3:
                    //ˆ—
                    break;
                case 4:
                    //ˆ—
                    break;
                case 5:
                    //ˆ—
                    break;

                default:
                    Debug.Log("Default");
                    break;

            }
        
        }

        public async UniTaskVoid TankPattern(GameObject TankEnemy, GameObject Enemy,Vector3 generatePos) //async‚É‚·‚é
        {
            Instantiate(TankEnemy, generatePos, Quaternion.identity);

            for (int i = 0; i < 2; i++)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));
                Instantiate(Enemy, generatePos, Quaternion.identity);
            }

        }
        public void AllPattern(int generateEnemyNum)
        {

            for (int i = 0; i < 3; i++)
            {
                Instantiate(TankEnemy, generatePos, Quaternion.identity);
            }

        }*/
    }
}