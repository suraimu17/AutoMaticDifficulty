using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Image shopPanel;
        [SerializeField] private Image charaPanel;
        public bool IsOpen { get; private set; } = false;

        private void Update()
        {

                if (Input.GetKeyDown(KeyCode.Space) && !shopPanel.gameObject.activeSelf&&!charaPanel.gameObject.activeSelf)
                {
                    OpenShopPanel();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && shopPanel.gameObject.activeSelf)
                {
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