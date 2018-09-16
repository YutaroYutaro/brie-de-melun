using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ManaTextPresenter : MonoBehaviour
{
    private Text _manaText;

    // Use this for initialization
    void Start()
    {
        _manaText = GetComponent<Text>();

        ManaModel.Instance.ManaReactiveProperty
            .Subscribe(
                mana => { _manaText.text = mana.ToString() + "/3"; }
            );
    }
}