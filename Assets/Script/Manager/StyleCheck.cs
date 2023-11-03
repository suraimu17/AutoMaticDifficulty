using System.Collections.Generic;
using UnityEngine;
using Manager;

public class StyleCheck : MonoBehaviour
{
    private CharaManager charaManager => CharaManager.Instance;
    private CoinManager coinManager => CoinManager.Instance;

    public float amountStylePer { private set; get; } = 0;
    public float strongStylePer { private set; get; } = 0;
    public float saveStylePer { private set; get; } = 0;

    private float coinPer = 0;

    //１つのパターンに決めるときだけ使う
    public bool amountStyle { private set; get; } = false;
    public bool strongStyle { private set; get; } = false;
    public bool saveStyle { private set; get; } = false;
    public bool IsStyle { private set; get; } = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CheckAmount();
            CheckStrong();
            CheckSave();
        }
    }
    public void CheckStyle()
    {
        ResetStyle();

        CheckAmount();
        CheckStrong();
        CheckSave();

        CalStylePer();
        Debug.Log("量スタイル割合:" + amountStylePer);
        Debug.Log("要所スタイル割合:" + strongStylePer);
        Debug.Log("様子見スタイル割合:" + saveStylePer);
        
        //１つのパターンに決めるときだけ使う
        if (amountStylePer > saveStylePer)
        {
            if (amountStylePer > strongStylePer) amountStyle = true;
            else strongStyle = true;

        }
        else if (strongStylePer > saveStylePer) strongStyle = true;
        else saveStyle = true;
        
    }   
    //どれだけキャラを置いているか
    private void CheckAmount() 
    {
        float charaNum = charaManager.setCharaNum;

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
        float upgradeNum = charaManager.upgradeNum;
        //強さ
        if (upgradeNum < 3)
        {
            strongStylePer += (upgradeNum / 4) / 4;
        }
        else
        {
            strongStylePer += (upgradeNum / 4) / 4;
            if (strongStylePer >= 1) strongStylePer = 0.5f;
        }
        //いい所
        float strongCharaNum = charaManager.strongTileChara;

        if (strongCharaNum < 4)
        {
            strongStylePer += (strongCharaNum / 5) / 4;
        }
        else 
        {
            strongStylePer += (upgradeNum / 5) / 4;
            if (strongStylePer >= 1) strongStylePer = 0.5f;
        }
        Debug.Log("要所スタイル" + strongStylePer);
    }   
    //コインをためているか
    public void CheckSave() 
    {

        coinPer += coinManager.CalCoinPer();
        //平均化する
        coinPer /= 2;
        Debug.Log("コイン割合"+coinPer);
        if (coinPer < 0.3f)
        {
            saveStylePer = coinPer / 0.7f;
        }
        else
        {
            saveStylePer = coinPer / 0.7f;
            if (saveStylePer >= 1) saveStylePer = 1.0f;
        }
        Debug.Log("様子見スタイル" + saveStylePer);
    }
    public void checkCoinPer() 
    {
        coinPer += coinManager.CalCoinPer();
    }
    private void ResetStyle()
    {
        amountStyle = false;
        strongStyle = false;
        saveStyle = false;
    }

    private void CalStylePer() 
    {
        if (amountStylePer < 0.3f && strongStylePer < 0.3f && saveStylePer < 0.3f) IsStyle = false;

        var sumPer = amountStylePer + strongStylePer + saveStylePer;
        amountStylePer = amountStylePer / sumPer;
        strongStylePer = strongStylePer / sumPer;
        saveStylePer = saveStylePer / sumPer;
    }

    //文字列を返す
    public string GetStyle() 
    {
        if (amountStyle == true) return "AmountStyle";
        else if (strongStyle == true) return "StrongStyle";
        else if (saveStyle == true) return "SaveStyle";

        return null;
    }
}
