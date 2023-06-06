using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Enemy.Test
{
    public class TestEnemyHp : MonoBehaviour, IEnemyHp
    {
        public int enemyHp { private set; get; }
        [SerializeField]private int baseHp;
        public bool IsDead => enemyHp <= 0;
        private void Start()
        {
            enemyHp = baseHp;
            DeathObservable();
            HpBarObservable();
        }
        public void DecreaseHp(int facilityPower)
        {
            enemyHp -= facilityPower;
            Debug.Log("�_���[�W");
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

        private void HpBarObservable()//TODO ��ŕ�������
        {
            GameObject child = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;//TODO�@��ŒT�����ς���

            if (child == null) Debug.Log("childnull");
            this.ObserveEveryValueChanged(_ => enemyHp)
                .Where(_ => enemyHp !=baseHp)//�ŏ��ɔ������Ȃ��悤��
                .Subscribe(_ =>
                {
                    if (!child.activeSelf) child.SetActive(true);

                    var hpBar = child.GetComponent<Slider>();
                    hpBar.value = (float)enemyHp / (float)baseHp;
                    Debug.Log((float)enemyHp / (float)baseHp);

                })
                .AddTo(this);
        }
    }
}