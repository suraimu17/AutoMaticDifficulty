using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Enemy;

namespace Manager
{

    public class DifficultyManager : MonoBehaviour
    {
        public float difficulty { get; private set; }=0.5f;
        [SerializeField] AnimationCurve animationCoinCurve;
        [SerializeField] AnimationCurve animationHpCurve;

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
            enemyGenerateManager.SetReleaseEnemy(difficulty);
        }


        public void adjustDifficulty() 
        {
           // var charaNum = charaManager.setCharaNum;
            var coin = coinManager.CurrentCoin;

            var coinPer = (float)coin / (float)(enemyGenerateManager.maxCoin+10);
            var hpPer = ((float)baseHp.currentBaseHp / (float)baseHp.MaxBaseHp); ;
            Debug.Log("coinPer"+coinPer);
            Debug.Log("hpPer"+hpPer);

            difficulty +=( (animationCoinCurve.Evaluate(coinPer)-0.5f)+(animationHpCurve.Evaluate(hpPer)-0.5f))/2;
            Debug.Log("��Փx"+difficulty);
            Debug.Log("CoinPer"+(animationCoinCurve.Evaluate(coinPer) - 0.5f)/2+ "HPPer"+ (animationHpCurve.Evaluate(hpPer) - 0.5f)/2);

        }

        
    }
}