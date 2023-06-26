using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Image shopPanel;

    public void OpenShopPanel() 
    {
        shopPanel.gameObject.SetActive(true);
    }
    public void CloseShopPanel()
    {
        shopPanel.gameObject.SetActive(false);
    }

}
