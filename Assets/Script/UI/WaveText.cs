using UnityEngine;
using UnityEngine.UI;
using Manager;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace UI
{
    public class WaveText : MonoBehaviour
    {
        GameManager gameManager => GameManager.Instance;
        private Text waveText;

        private GameObject child;
        private void Start()
        {
            child = transform.GetChild(0).gameObject;
            waveText =child.GetComponent<Text>();

            CancellationToken token = this.GetCancellationTokenOnDestroy();

             WaveUIObservable(token);
        }
        private async UniTaskVoid WaveUIObservable(CancellationToken token)
        {
            child.SetActive(true);

            await UniTask.Delay(System.TimeSpan.FromSeconds(2.0f), cancellationToken: token);

            child.SetActive(false);

            await UniTask.WaitUntil(() => gameManager.waveNum == 2);

            waveText.text = "Wave " + gameManager.waveNum;

            child.SetActive(true);

            await UniTask.Delay(System.TimeSpan.FromSeconds(2.0f), cancellationToken: token);

            child.SetActive(false);
        }
    }
}