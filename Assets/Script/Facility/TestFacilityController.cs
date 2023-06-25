using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestFacilityController:MonoBehaviour,IFacilityController
{
    private TestFacilityAttack testFacilityAttack;
    private FacilitySearch facilitySearch;

    private GameObject targetEnemy;

    private int attackPower = 2;

    private void Start()
    {
        testFacilityAttack = GetComponent<TestFacilityAttack>();
        facilitySearch = GetComponent<FacilitySearch>();

        AttackObservable();
    }
    private void Update()
    {
        targetEnemy = facilitySearch.FindNearerstEnemy();
    }
    private void AttackObservable() 
    {
        this.UpdateAsObservable()
            .ThrottleFirst(System.TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                //targetEnemy = facilitySearch.FindNearerstEnemy();
                if (targetEnemy == null) return;
                Debug.Log("ìGÇÕÇ¢ÇΩ");

                testFacilityAttack.Attack(targetEnemy, attackPower);
                Debug.Log("çUåÇ");
            })
            .AddTo(this);
    }

}
