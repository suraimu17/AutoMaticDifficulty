using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Base;
using UnityEngine.UI;

namespace UI
{
    public class BaseHpBar : MonoBehaviour
    {
        private GameObject parent;
        private BaseHP baseHP;
        private void Start()
        {
            parent = transform.root.gameObject;
            baseHP = parent.GetComponent<BaseHP>();

            BaseHpBarObservable();
        }
        private void BaseHpBarObservable()
        {
            var maxBaseHp=baseHP.MaxBaseHp;

            //Debug.Log("base"+baseHP.currentBaseHp+"max"+maxBaseHp);
            this.ObserveEveryValueChanged(_ => baseHP.currentBaseHp)
               // .Where(_ => enemyHp != baseHp)//Å‰‚É”½‰ž‚µ‚È‚¢‚æ‚¤‚É
                .Subscribe(_ =>
                {
                    var baseHpBar = gameObject.GetComponent<Slider>();
                    baseHpBar.value = (float)baseHP.currentBaseHp / (float)parent.GetComponent<BaseHP>().MaxBaseHp;

                })
                .AddTo(this);
        }
    }
}