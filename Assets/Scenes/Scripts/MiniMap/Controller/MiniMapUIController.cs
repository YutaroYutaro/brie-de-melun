using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class MiniMapUIController : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    // Use this for initialization
    void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();
        _rectTransform = GetComponent<RectTransform>();

        float posX = _rectTransform.localPosition.x;
        float posY = _rectTransform.localPosition.y;

        observableEventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ =>
                {
                    _rectTransform.DOScale(Vector3.one * 1.3f, 0.5f);
                    _rectTransform.DOLocalMove(new Vector3(posX + 60, posY - 70), 0.5f);
                }
            );

        observableEventTrigger.OnPointerExitAsObservable()
            .Subscribe(_ =>
                {
                    _rectTransform.DOScale(Vector3.one, 0.5f);
                    _rectTransform.DOLocalMove(new Vector3(posX, posY), 0.5f);
                }
            );
    }
}