using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestEnemyHp : MonoBehaviour,IEnemyHp
{
    public int enemyHp { private set;  get; } = 10;

    public bool IsDead => enemyHp <= 0;
    private void Start()
    {
        DeathObservable();
    }
    public void DecreaseHp(int facilityPower) 
    {
        enemyHp -= facilityPower;
        Debug.Log("ƒ_ƒ[ƒW");
    }

    private void DeathObservable() 
    {
        this.ObserveEveryValueChanged(_ => IsDead)
            .Where(_ => IsDead==true)
            .Subscribe(_ =>
            {
                Destroy(this.gameObject);
            })
            .AddTo(this);
    }
}
