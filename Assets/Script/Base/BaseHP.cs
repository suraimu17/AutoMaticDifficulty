using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base {
    public class BaseHP : MonoBehaviour
    {
        public int baseHp { private set; get; } = 10;

        public void DecreaseHp ()
        {
            baseHp--;
            Debug.Log(baseHp);
        }
    }
}