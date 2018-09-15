using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class ManaPresenter : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

        observableEventTrigger.OnDropAsObservable()
            .Subscribe(eventData =>
                ManaModel.Instance.ManaReactiveProperty.Value -=
                    eventData.pointerDrag.GetComponent<ManaOfCard>().Mana
            );
    }
}