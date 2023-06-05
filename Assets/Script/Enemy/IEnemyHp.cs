using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyHp
    {
        int enemyHp { get; }
        bool IsDead { get; }
        void DecreaseHp(int facilityPower);
    }
}