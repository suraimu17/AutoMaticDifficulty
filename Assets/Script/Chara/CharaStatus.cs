using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class CharaStatus : MonoBehaviour,ICharaStatus
{

    [field:SerializeField] public float power { get; private set; }
    [field:SerializeField] public int cost { get; private set; }
   // [SerializeField]private int nextCost;
     public int level { get; private set; } = 1;






    public void Upgrade() 
    {
        if (CoinManager.Instance.BuyChara(cost) == true)
        {
            power *= 1.5f;
            level++;
        }
    }




}
