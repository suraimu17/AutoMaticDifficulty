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

        [SerializeField] private Sprite cSprite;

        public void OpenCharaPanel(GameObject chara)
        {

            charaPanel.gameObject.SetActive(true);
            var charaStatus = chara.gameObject.GetComponent<CharaStatus>();

            charaImage.sprite = chara.gameObject.GetComponent<SpriteRenderer>().sprite;
            powerText.text = "攻撃力：" + charaStatus.power;
            nameText.text = "" + chara.name;
            upgradeText.text = "次のコスト" + charaStatus.cost;
            upgradeButton.onClick.AddListener(charaStatus.Upgrade);

        }

    }
}