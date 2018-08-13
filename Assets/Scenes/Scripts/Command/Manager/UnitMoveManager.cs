using UnityEngine;

public class UnitMoveManager : MonoBehaviour
{
    public GameObject SelectMoveUnit;

    public void SetMoveUnit(GameObject selectMoveUnit)
    {
        SelectMoveUnit = selectMoveUnit;
    }

    public void MiniMapUnitMove(
        int clickMiniMapImageInstancePositionX,
        int clickMiniMapImageInstancePositionZ
    )
    {
        SelectMoveUnit
            .GetComponent<UnitMove>()
            .MiniMapClickUnitMove(
                clickMiniMapImageInstancePositionX,
                clickMiniMapImageInstancePositionZ
            );
    }
}