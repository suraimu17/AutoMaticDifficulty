using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHp
{
    int enemyHp { get; }
    bool IsDead { get; }
    void DecreaseHp(int facilityPower);
}
