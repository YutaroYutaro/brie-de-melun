using UnityEngine;
using System.Threading.Tasks;
using UniRx.Async;

public class UnitMoveManager : MonoBehaviour
{
    public GameObject SelectMoveUnit;

    public void SetMoveUnit(GameObject selectMoveUnit)
    {
        SelectMoveUnit = selectMoveUnit;
    }

    public async UniTask<bool> MiniMapUnitMove(
        int clickMiniMapImageInstancePositionX,
        int clickMiniMapImageInstancePositionZ
    )
    {
        return await SelectMoveUnit
            .GetComponent<UnitMove>()
            .MiniMapClickUnitMove(
                clickMiniMapImageInstancePositionX,
                clickMiniMapImageInstancePositionZ
            );
    }
}