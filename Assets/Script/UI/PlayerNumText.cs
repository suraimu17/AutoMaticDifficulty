using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class PlayerNumText : MonoBehaviour
{
    private Text playerNumText;

    private void Start()
    {
        playerNumText = GetComponent<Text>();

        GameManager gameManager = GameManager.Instance;

        playerNumText.text = ""+gameManager.playerNum+"l–Ú";
    }
}
