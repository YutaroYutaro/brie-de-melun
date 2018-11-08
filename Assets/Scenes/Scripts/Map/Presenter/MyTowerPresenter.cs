using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class MyTowerPresenter : MonoBehaviour
{
    void Start()
    {
        MyTowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint == 2
            )
            .Subscribe(_ =>
                {
                    Debug.Log("Tower Break!");
                    GetComponent<TowerAnimator>().FirstDestroy();
                    Observable.Timer(TimeSpan.FromMilliseconds(2000))
                        .Subscribe(__ =>
                            {
                                Camera.main.gameObject.transform.DOMoveY(2.5f, 0.5f);
                                Camera.main.gameObject.transform.DORotate(new Vector3(25f,0), 0.5f);
                            }

                        );
                }
            );

        MyTowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint == 1
            )
            .Subscribe(_ =>
                {
                    Debug.Log("Tower Break!");
                    GetComponent<TowerAnimator>().SecondDestroy();
                    Observable.Timer(TimeSpan.FromMilliseconds(2000))
                        .Subscribe(__ =>
                            {
                                Camera.main.gameObject.transform.DOMoveY(1f, 0.5f);
                                Camera.main.gameObject.transform.DORotate(new Vector3(0,0), 0.5f);
                            }
                        );
                }
            );

        MyTowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint <= 0
            )
            .Subscribe(_ =>
                {
                    GetComponent<TowerAnimator>().ThirdDestroy();
                    Observable.Timer(TimeSpan.FromMilliseconds(2000))
                        .Subscribe(__ =>
                            {
                                Camera.main.gameObject.transform.DOMoveY(-10f, 0.5f);
                                Camera.main.gameObject.transform.DORotate(new Vector3(-70f,0), 0.5f);
                            }
                        );
                }
            );
    }
}