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
                towerHitPoint == 2 || towerHitPoint == 1
            )
            .Subscribe(_ =>
                Debug.Log("Tower Break!")
            );

        TowerModel.Instance.TowerHitPointReactiveProperty
            .Where(towerHitPoint =>
                towerHitPoint <= 0
            )
            .Subscribe(_ =>
                {
                    EnemyUnitController.Instance.GameEndRpc();
                    GameEndManager.Instance.GameEnd();
                }
            );
    }
}