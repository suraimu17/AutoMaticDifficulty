using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Manager;

namespace UI
{
    public class TimeText : MonoBehaviour
    {
        private Text timeText;
        private int time => GameManager.Instance.currentTime;
        private void Start()
        {
            timeText = GetComponent<Text>();
            TimeUIObservable();
        }
        private void TimeUIObservable()
        {
            this.ObserveEveryValueChanged(x => time)
                .Subscribe(_=>
                {
                    timeText.text = "�c�莞�ԁ@" + time;
                })
                .AddTo(this);

        }
    }
}