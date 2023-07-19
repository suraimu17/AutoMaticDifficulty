using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyHp
    {
        float enemyHp { get; }
        bool IsDead { get; }
        void DecreaseHp(float charaPower);
    }
}