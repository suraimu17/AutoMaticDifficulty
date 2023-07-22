using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class CharaStatus : MonoBehaviour,ICharaStatus
{

    [field:SerializeField] public float power { get; private set; }
    [field:SerializeField] public int upgradeCost { get; private set; }
    [field:SerializeField] public int buyCost { get; private set; }

    [SerializeField] private GameObject upgradeObj;
   // [SerializeField]private int nextCost;
     public int level { get; private set; } = 1;






    public void Upgrade() 
    {
        if (CoinManager.Instance.BuyChara(upgradeCost) == true)
        {
            power *= 1.5f;
            level++;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = upgradeObj.GetComponent<SpriteRenderer>().sprite;
        }
    }




}
