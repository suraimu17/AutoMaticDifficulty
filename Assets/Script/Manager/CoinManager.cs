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

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        private void BuyFacility(int price)
        {
            if (CurrentCoin - price < 0)
            {
                Debug.Log("お金足りないよ");
                return;
            }

            //施設を置く処理



        }

        // 敵につける処理
        public void DropCoin(int DropNum)
        {
            CurrentCoin += DropNum;
            Debug.Log("coin"+CurrentCoin);
        }
    }
}