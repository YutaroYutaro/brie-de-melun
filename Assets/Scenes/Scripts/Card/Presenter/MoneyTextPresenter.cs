using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MoneyTextPresenter : MonoBehaviour
{
    private Text _moneyText;

    void Start()
    {
        _moneyText = GetComponent<Text>();

        MoneyModel.Instance.MoneyReactiveProperty
            .Subscribe(money =>
                _moneyText.text = money.ToString()
            );
    }
}