using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TowerPresenter : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        TowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint == 2
            )
            .Subscribe(_ =>
                {
                    Debug.Log("Tower Break!");
                    GetComponent<TowerAnimator>().FirstDestroy();
                }
            );

        TowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint == 1
            )
            .Subscribe(_ =>
                {
                    Debug.Log("Tower Break!");
                    GetComponent<TowerAnimator>().SecondDestroy();
                }
            );

        TowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint <= 0
            )
            .Subscribe(_ =>
                {
                    GetComponent<TowerAnimator>().ThirdDestroy();
                    EnemyUnitController.Instance.GameEndRpc();
                    GameEndManager.Instance.GameEnd();
                }
            );
    }
}