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
        private int baseCoin = 5;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            CurrentCoin = baseCoin;

            DontDestroyOnLoad(gameObject);
        }

        public bool BuyFacility(int price)
        {
            if (CurrentCoin - price < 0)
            {
                Debug.Log("‚¨‹à‘«‚è‚È‚¢‚æ");
                return false;
            }
            CurrentCoin -= price;

            return true;
        }

        // “G‚É‚Â‚¯‚éˆ—
        public void DropCoin(int DropNum)
        {
            CurrentCoin += DropNum;
            Debug.Log("coin"+CurrentCoin);
        }
    }
}