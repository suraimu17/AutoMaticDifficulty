using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class CharaStatus : MonoBehaviour,ICharaStatus
{

    [field:SerializeField] public int power { get; private set; }
    [field:SerializeField] public int upgradeCost { get; private set; }
    [field:SerializeField] public int buyCost { get; private set; }
    [field:SerializeField] public int multiPower { get; private set; }

    [SerializeField] private GameObject upgradeObj;
   //[SerializeField]private int nextCost;
     public int level { get; private set; } = 1;

    private CharaManager charaManager => CharaManager.Instance;

    public void Upgrade() 
    {
        if (CoinManager.Instance.BuyChara(upgradeCost) == true)
        {
            power =multiPower;
            level++;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = upgradeObj.GetComponent<SpriteRenderer>().sprite;
            charaManager.upgradeNum++;
        }
    }




}
