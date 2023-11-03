using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Enemy;
using System.Linq;

namespace Manager
{

    public class DifficultyManager : MonoBehaviour
    {

        private const float baseDifficulty = 0f;
        public float difficulty { get; private set; }=baseDifficulty;
        [SerializeField] AnimationCurve animationCoinCurve;//y=x
        [SerializeField] AnimationCurve animationHpCurve;//y=x*x

        private BaseHP baseHp;

        private CoinManager coinManager => CoinManager.Instance;
        private CharaManager charaManager => CharaManager.Instance;

        private EnemyGenerateManager enemyGenerateManager;
        private void Start()
        {
            baseHp = FindObjectOfType<BaseHP>();
            enemyGenerateManager = FindObjectOfType<EnemyGenerateManager>();
        }

        public void reflectDifficulty() 
        {
            adjustDifficulty();
            enemyGenerateManager.SetGenerateSpan(difficulty);
        }


        public void adjustDifficulty() 
        {
            // var charaNum = charaManager.setCharaNum
            var average = enemyGenerateManager.aliveTimeList.Average();
            var result = -((average / 28) * (average / 28)) + 1;
            Debug.Log("timeAverage"+result);

            var coinPer = coinManager.CalCoinPer();
            var hpPer = ((float)baseHp.currentBaseHp / (float)baseHp.MaxBaseHp); ;
            Debug.Log("hpPer"+hpPer);

            difficulty =(animationCoinCurve.Evaluate(coinPer)*0.4f)+(animationHpCurve.Evaluate(hpPer)*0.4f)+result*0.2f;
            Debug.Log("“ïˆÕ“x"+difficulty);
            Debug.Log("CoinPer"+ (animationCoinCurve.Evaluate(coinPer) * 0.4f) + "HPPer"+ (animationHpCurve.Evaluate(hpPer) * 0.4f)+"TimePer"+result*0.2f);
        }

        
    }
}