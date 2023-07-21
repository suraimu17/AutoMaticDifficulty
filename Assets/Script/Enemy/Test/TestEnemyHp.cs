using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Manager;


namespace Enemy.Test
{
    public class TestEnemyHp : MonoBehaviour, IEnemyHp
    {
        public float enemyHp { private set; get; }
        [SerializeField]private int baseHp;
        public bool IsDead => enemyHp <= 0;
        private int coinNum=> GetComponent<TestEnemyController>().coinNum;

        private CoinManager coinManager => CoinManager.Instance;
        private void Start()
        {
            enemyHp = baseHp;
            DeathObservable();
            HpBarObservable();
        }
        public void DecreaseHp(float charaPower)
        {
            enemyHp -= charaPower;
            //Debug.Log("ダメージ");

            if (enemyHp <= 0) coinManager.DropCoin(coinNum);
        }

        private void DeathObservable()
        {
            this.ObserveEveryValueChanged(_ => IsDead)
                .Where(_ => IsDead == true)
                .Subscribe(_ =>
                {
                    Destroy(this.gameObject);
                })
                .AddTo(this);
        }

        private void HpBarObservable()//TODO 後で分割する
        {
            GameObject child = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;//TODO　後で探し方変える

            if (child == null) Debug.Log("childnull");
            this.ObserveEveryValueChanged(_ => enemyHp)
                .Where(_ => enemyHp !=baseHp)//最初に反応しないように
                .Subscribe(_ =>
                {
                    if (!child.activeSelf) child.SetActive(true);

                    var hpBar = child.GetComponent<Slider>();
                    hpBar.value = (float)enemyHp / (float)baseHp;

                })
                .AddTo(this);
        }
    }
}