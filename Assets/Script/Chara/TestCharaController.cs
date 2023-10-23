using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestCharaController:MonoBehaviour,ICharaController
{
    private ICharaAttack charaAttack;
    private CharaSearch charaSearch;
    private CharaStatus charaStatus;

    private GameObject targetEnemy;

    private void Start()
    {
        charaAttack = GetComponent<ICharaAttack>();
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

                charaAttack.Attack(targetEnemy, charaStatus.power);
                //Debug.Log("çUåÇ");
            })
            .AddTo(this);
    }

}
