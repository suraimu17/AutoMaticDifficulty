using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Manager;

namespace UI
{
    public class WaveText : MonoBehaviour
    {
        GameManager gameManager => GameManager.Instance;
        private Text waveText;
        private void Start()
        {
            waveText = GetComponent<Text>();
            WaveUIObservable();
        }
        private void WaveUIObservable()
        {
            this.ObserveEveryValueChanged(_ => gameManager.waveNum)
                .Subscribe(_ =>
                {
                    waveText.text = "Wave:" + gameManager.waveNum;
                })
                .AddTo(this);

        }
    }
}