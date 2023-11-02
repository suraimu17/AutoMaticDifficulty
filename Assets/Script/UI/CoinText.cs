using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Manager;

public class CoinText : MonoBehaviour
{ 
    private int coin => CoinManager.Instance.CurrentCoin;

    private Text coinText;
    private void Start()
    { 
        coinText=GetComponent<Text>();
        CoinUIObservable();
    }
    private void CoinUIObservable() 
    {
        this.ObserveEveryValueChanged(_=>coin)
            .Subscribe(_ =>
            {
                coinText.text =""+ coin;
            })
            .AddTo(this);
    
    }
}
