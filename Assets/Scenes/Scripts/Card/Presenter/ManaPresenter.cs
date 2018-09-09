using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using UniRx.Async.Triggers;
using UniRx.Triggers;
using UniRx;
using UnityEngine.EventSystems;
using Asset.Scripts.Mana;

public class ManaPresenter : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

        observableEventTrigger.OnDropAsObservable()
            .Subscribe(eventData =>
                GetComponent<ManaModel>().ManaReactiveProperty.Value -=
                    eventData.pointerDrag.GetComponent<ManaOfCard>().Mana
            );

        GetComponent<ManaModel>().ManaReactiveProperty
            .Subscribe(mana =>
                {
                    Debug.Log("Rest of Mana: " + mana);
                }
            );
    }
}