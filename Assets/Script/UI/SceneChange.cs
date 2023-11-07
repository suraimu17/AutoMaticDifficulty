using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Base;

public class SceneChange : MonoBehaviour
{
    private BaseHP baseHP;
    private void Start()
    {
        baseHP = FindObjectOfType<BaseHP>();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("ExplanationScene");
            }
        }

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (baseHP.currentBaseHp <= 0) 
            {
                SceneManager.LoadScene("EndScene");
            }
        }
    }
   


}
