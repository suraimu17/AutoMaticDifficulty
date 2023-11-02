using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Manager
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager Instance = null;
        public int CurrentCoin { get; private set; }
        public int baseCoin { get; private set; } = 10;
        //�E�F�[�u�Ŏ�ɓ������R�C��
        public int canGetCoin = 0;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            CurrentCoin = baseCoin;

            DontDestroyOnLoad(gameObject);
        }

        public bool BuyChara(int price)
        {
            if (CurrentCoin - price < 0)
            {
                Debug.Log("��������Ȃ���");
                return false;
            }
            CurrentCoin -= price;

            return true;
        }

        // �G�ɂ��鏈��
        public void DropCoin(int DropNum)
        {
            CurrentCoin += DropNum;
        }

        public float CalCoinPer() 
        {
            return (float)CurrentCoin / (baseCoin + canGetCoin);
        }
    }
}