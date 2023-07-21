using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestCharaController:MonoBehaviour,ICharaController
{
    private TestCharaAttack testCharaAttack;
    private CharaSearch charaSearch;
    private CharaStatus charaStatus;

    private GameObject targetEnemy;

    private void Start()
    {
        testCharaAttack = GetComponent<TestCharaAttack>();
        charaSearch = GetComponent<CharaSearch>();
        charaStatus = GetComponent<CharaStatus>();

        AttackObservable();
    }
    private void Update()
    {
        targetEnemy = charaSearch.FindNearerstEnemy();
    }
    private void AttackObservable() 
    {
        this.UpdateAsObservable()
            .ThrottleFirst(System.TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                //targetEnemy = facilitySearch.FindNearerstEnemy();
                if (targetEnemy == null) return;
                //Debug.Log("ìGÇÕÇ¢ÇΩ");

                testCharaAttack.Attack(targetEnemy, charaStatus.power);
                //Debug.Log("çUåÇ");
            })
            .AddTo(this);
    }

}
