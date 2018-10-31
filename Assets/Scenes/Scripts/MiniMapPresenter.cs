using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class MiniMapPresenter : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    // Use this for initialization
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        float posX = _rectTransform.localPosition.x;
        float posY = _rectTransform.localPosition.y;

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase != "SelectUseCard" && phase != "EnemyTurn"
            )
            .Subscribe(phase =>
                {
                    _rectTransform.DOScale(Vector3.one * 1.3f, 0.5f);
                    _rectTransform.DOLocalMove(new Vector3(posX + 60, posY - 70), 0.5f);
                }
            );

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectUseCard"
            )
            .Subscribe(phase =>
                {
                    _rectTransform.DOScale(Vector3.one, 0.5f);
                    _rectTransform.DOLocalMove(new Vector3(posX, posY), 0.5f);
                }
            );
    }
}