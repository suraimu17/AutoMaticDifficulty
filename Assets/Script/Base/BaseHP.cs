using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base {
    public class BaseHP : MonoBehaviour
    {
        public int MaxBaseHp { private set; get; } = 10;
        public int currentBaseHp { private set; get; }
        private void Awake()
        {
            resetHp();
        }
        public void DecreaseHp ()
        {
            currentBaseHp--;
        }
        public void resetHp()
        {
            currentBaseHp = MaxBaseHp;
        }
    }
}