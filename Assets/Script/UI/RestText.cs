using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Manager;

namespace UI
{
    public class RestText : MonoBehaviour
    {
        private Text restText;

        private EnemyGenerateManager enemyGenerateManager;
        private void Start()
        {
            enemyGenerateManager = FindObjectOfType<EnemyGenerateManager>();
            restText = GetComponent<Text>();
            RestUIObservable();
        }
        private void RestUIObservable()
        {
            this.ObserveEveryValueChanged(x => enemyGenerateManager.generateCount)
                .Subscribe(_=>
                {
                    restText.text = "c‚è“G”F" + enemyGenerateManager.generateCount;
                })
                .AddTo(this);

        }
    }
}