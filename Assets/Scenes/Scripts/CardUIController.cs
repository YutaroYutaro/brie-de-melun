using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardUIController : MonoBehaviour
{
    [SerializeField] private bool _isSelected;
//    [SerializeField] private bool _isDraged;
    private RectTransform _rectTransform;

    // Use this for initialization
    void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();
        _rectTransform = GetComponent<RectTransform>();

        observableEventTrigger.OnBeginDragAsObservable()
            .Subscribe(_ =>
                PointerManager.Instance.IsDraged = true
            );

        observableEventTrigger.OnEndDragAsObservable()
            .Subscribe(_ =>
                PointerManager.Instance.IsDraged = false
            );

        observableEventTrigger.OnPointerEnterAsObservable()
            .Where(_ =>
                !_isSelected && !PointerManager.Instance.IsDraged
            )
            .Subscribe(_ =>
                {
                    _isSelected = true;
                    _rectTransform.DOLocalMoveY(_rectTransform.position.y + 100, 0.5f);
                }
            );

        observableEventTrigger.OnPointerExitAsObservable()
            .Where(_ =>
                _isSelected && !PointerManager.Instance.IsDraged
            )
            .Subscribe(_ =>
                {
                    _isSelected = false;
                    _rectTransform.DOLocalMoveY(_rectTransform.position.y, 0.5f);
                }
            );
    }
}