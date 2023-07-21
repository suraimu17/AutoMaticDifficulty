using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Image shopPanel;
        public bool IsOpen { get; private set; } = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OpenShopPanel();
            }
            else if (Input.GetKeyDown(KeyCode.Q)&&shopPanel.gameObject.activeSelf)
            {
                Debug.Log("close");
                CloseShopPanel();
            }
        }
        public void OpenShopPanel()
        {
            shopPanel.gameObject.SetActive(true);
            IsOpen = true;
        }
        public void CloseShopPanel()
        {
            shopPanel.gameObject.SetActive(false);
            IsOpen = false;
        }

    }
}