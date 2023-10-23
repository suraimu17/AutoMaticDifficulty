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

    //�P�̃p�^�[���Ɍ��߂�Ƃ������g��
    public bool amountStyle { private set; get; } = false;
    public bool strongStyle { private set; get; } = false;
    public bool saveStyle { private set; get; } = false;

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
        Debug.Log("�ʃX�^�C������:" + amountStylePer);
        Debug.Log("�v���X�^�C������:" + strongStylePer);
        Debug.Log("�l�q���X�^�C������:" + saveStylePer);
        /*
        //�P�̃p�^�[���Ɍ��߂�Ƃ������g��
        if (amountStylePer > saveStylePer)
        {
            if (amountStylePer > strongStylePer) amountStyle = true;
            else strongStyle = true;

        }
        else if (strongStylePer > saveStylePer) strongStyle = true;
        else saveStyle = true;
        */
    }   
    //�ǂꂾ���L������u���Ă��邩
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
        Debug.Log("�ʃX�^�C��" + amountStylePer);
    }
    //�������ɂ����Ă��邩�⋭�����Ă��邩
    private void CheckStrong() 
    {
        float upgradeNum = charaManager.upgradeNum;
        //����
        if (upgradeNum < 4)
        {
            strongStylePer += (upgradeNum / 4) / 4;
        }
        else
        {
            strongStylePer += (upgradeNum / 4) / 4;
            if (strongStylePer >= 1) strongStylePer = 0.5f;
        }
        //������
        float strongCharaNum = charaManager.strongTileChara;

        if (strongCharaNum < 5)
        {
            strongStylePer += (strongCharaNum / 5) / 4;
        }
        else 
        {
            strongStylePer += (upgradeNum / 5) / 4;
            if (strongStylePer >= 1) strongStylePer = 0.5f;
        }
        Debug.Log("�v���X�^�C��" + strongStylePer);
    }   
    //�R�C�������߂Ă��邩
    private void CheckSave() 
    {
        
        float coinPer = coinManager.CalCoinPer();
        Debug.Log("�R�C������"+coinPer);
        if (coinPer < 0.45f)
        {
            saveStylePer = coinPer / 0.8f;
        }
        else
        {
            saveStylePer = coinPer / 0.8f;
            if (saveStylePer >= 1) saveStylePer = 1.0f;
        }
        Debug.Log("�l�q���X�^�C��" + saveStylePer);
    }
    private void ResetStyle()
    {
        amountStyle = false;
        strongStyle = false;
        saveStyle = false;
    }

    private void CalStylePer() 
    {
        if (amountStylePer < 0.4f && strongStylePer < 0.4f && saveStylePer < 0.4f) Debug.Log("�ǂ�����Ă͂܂�Ȃ�");

        var sumPer = amountStylePer + strongStylePer + saveStylePer;
        amountStylePer = amountStylePer / sumPer;
        strongStylePer = strongStylePer / sumPer;
        saveStylePer = saveStylePer / sumPer;
    }
}
