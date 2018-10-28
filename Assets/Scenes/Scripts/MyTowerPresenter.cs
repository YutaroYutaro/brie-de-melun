using System.Collections;
using System.Collections.Generic;
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
                }
            );

        MyTowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint <= 0
            )
            .Subscribe(_ => { GetComponent<TowerAnimator>().ThirdDestroy(); }
            );
    }
}