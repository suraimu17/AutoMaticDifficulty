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
    }
    public void ChoiceChara2()
    {
        charaManager.ResetCharaChoice();
        charaManager.CharaChoise[1] = true;
    }
    public void ChoiceChara3()
    {
        charaManager.ResetCharaChoice();
        charaManager.CharaChoise[2] = true;
    }
}
