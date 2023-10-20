using System.Collections.Generic;
using UnityEngine;
using Manager;

public class StyleCheck : MonoBehaviour
{
    private CharaManager charaManager => CharaManager.Instance;
    private CoinManager coinManager => CoinManager.Instance;

    public float amountStylePer { private set; get; }
    public float strongStylePer { private set; get; }
    public float saveStylePer { private set; get; }

    //１つのパターンに決めるときだけ使う
    public bool amountStyle { private set; get; } = false;
    public bool strongStyle { private set; get; } = false;
    public bool saveStyle { private set; get; } = false;

    private void CheckStyle()
    {
        ResetStyle();

        CheckAmount();
        CheckStrong();
        CheckSave();

        /*
        //１つのパターンに決めるときだけ使う
        if (amountStylePer > saveStylePer)
        {
            if (amountStylePer > strongStylePer) amountStyle = true;
            else strongStyle = true;

        }
        else if (strongStylePer > saveStylePer) strongStyle = true;
        else saveStyle = true;
        */
    }   
    //どれだけキャラを置いているか
    private void CheckAmount() 
    {
        var charaNum = charaManager.setCharaNum;

        if (charaNum <10)
        {
            amountStylePer = (charaNum / 10) / 2;
        }
        else
        {
            amountStylePer = (charaNum / 10)/2;
            if (amountStylePer >= 1) amountStylePer = 1.0f;
        }
        Debug.Log("量スタイル" + amountStylePer);
    }
    //いい所においているかや強くしているか
    private void CheckStrong() 
    {
        var upgradeNum = charaManager.upgradeNum;
        //強さ
        if (upgradeNum < 5)
        {
            strongStylePer += (upgradeNum / 5) / 4;
        }
        else
        {
            strongStylePer += (upgradeNum / 5) / 4;
            if (amountStylePer >= 1) amountStylePer = 0.5f;
        }
        //いい所

    }   
    //コインをためているか
    private void CheckSave() 
    {
        
        var coinPer = coinManager.CalCoinPer();
        if (coinPer < 0.5f)
        {
            saveStylePer = coinPer / 0.8f;
        }
        else
        {
            saveStylePer = coinPer / 0.8f;
            if (saveStylePer >= 1) saveStylePer = 1.0f;
        }
        Debug.Log("様子見スタイル" + saveStylePer);
    }
    private void ResetStyle()
    {
        amountStyle = false;
        strongStyle = false;
        saveStyle = false;
    }
}
