using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public int waveNum { get; private set; } = 1;
        public int currentTime { get; private set; } = WaveTime;

        private const int WaveTime= 60;

        public static GameManager Instance = null;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);


            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            TimeObservable();
        }
        private void TimeObservable() 
        {
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(1))
                //.Where(_=>)Waveˆ—’†‚Íœ‚­ˆ—‚ðŽÀ‘•‚·‚é
                .Subscribe(_ =>
                {
                    currentTime--;
                })
                .AddTo(this);
        }
    }
}