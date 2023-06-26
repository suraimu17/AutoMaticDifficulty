using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class CharaButton : MonoBehaviour
{
    private CharaManager charaManager=>CharaManager.Instance;
    public void ChoiceChara1()
    {
        charaManager.ResetCharaChoice();
        charaManager.CharaChoise[0] = true;
        Debug.Log("0" + charaManager.CharaChoise[0]);
    }
    public void ChoiceChara2()
    {
        charaManager.ResetCharaChoice();
        charaManager.CharaChoise[1] = true;
        Debug.Log("0" + charaManager.CharaChoise[1]);
    }
   /* public void ChoiceChara3()
    {
        charaManager.ResetCharaChoice();
        charaManager.CharaChoise[2] = true;
    }
    public void ChoiceChara4()
    {
        charaManager.ResetCharaChoice();
        charaManager.CharaChoise[3] = true;
    }*/
}
