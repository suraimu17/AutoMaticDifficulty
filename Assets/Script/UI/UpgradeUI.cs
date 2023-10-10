using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private Image charaPanel;
        [SerializeField] private Image charaImage;
        [SerializeField] private Text powerText;
        [SerializeField] private Text nameText;
        [SerializeField] private Text upgradeText;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button cancelButton;

        private void Update()
        {
            CancelButton();
        }
        public void OpenCharaPanel(GameObject chara)
        {
            upgradeButton.onClick.RemoveAllListeners();

            charaPanel.gameObject.SetActive(true);
            var charaStatus = chara.gameObject.GetComponent<CharaStatus>();
            if (charaStatus.level == 2) upgradeButton.gameObject.SetActive(false);
            else upgradeButton.gameObject.SetActive(true);
            UIUpdate(chara);
            upgradeButton.onClick.AddListener(charaStatus.Upgrade);

            //upgradeButton.onClick.AddListener(()=>UIUpdate(chara));



        }
        public void UIUpdate(GameObject chara)
        {
            var charaStatus = chara.gameObject.GetComponent<CharaStatus>();

            charaImage.sprite = chara.gameObject.GetComponent<SpriteRenderer>().sprite;
            powerText.text = "攻撃力：" + charaStatus.power;
            nameText.text = "" + chara.name;
            upgradeText.text = "次のコスト" + charaStatus.upgradeCost;

        }
        public void CancelButton()
        {
            if (cancelButton.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                charaPanel.gameObject.SetActive(false);
            }
        }
        public void Cancel() 
        {
            charaPanel.gameObject.SetActive(false);
        }
    }
}