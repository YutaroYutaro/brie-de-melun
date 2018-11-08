using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class CardEventTrigerInShopPresenter : MonoBehaviour
{
    public GameObject CardPrefab;

    void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

        observableEventTrigger.OnPointerDownAsObservable()
            .Where(eventData =>
                MoneyModel.Instance.MoneyReactiveProperty.Value >= CardPrefab.GetComponent<PriceOfCard>().Price)
            .Subscribe(eventData =>
                {
                    MoneyModel.Instance.MoneyReactiveProperty.Value -= CardPrefab.GetComponent<PriceOfCard>().Price;
                    HandModel.Instance.GenerateCard(CardPrefab);
                }
            );
    }
}