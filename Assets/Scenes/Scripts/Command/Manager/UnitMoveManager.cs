using UnityEngine;
using System.Threading.Tasks;
using UniRx.Async;

public class UnitMoveManager : MonoBehaviour
{
    public GameObject SelectMoveUnit;

    [SerializeField]
    private int _selectedUnitMovementPoint;

    public void SetMoveUnit(GameObject selectMoveUnit)
    {
        SelectMoveUnit = selectMoveUnit;

        _selectedUnitMovementPoint = (selectMoveUnit != null) ? selectMoveUnit.GetComponent<UnitStatus>().MovementPoint : 0;
    }

    public int SelectedUnitMovePoint => _selectedUnitMovementPoint;

    public void ConsumeSelectedUnitMovePoint(int consumeMovementPoint)
    {
        _selectedUnitMovementPoint -= consumeMovementPoint;
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