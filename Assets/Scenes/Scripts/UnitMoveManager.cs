using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveManager : MonoBehaviour
{
    public GameObject _selectMoveUnit = null;

    public void SetMoveUnit(GameObject selectMoveUnit)
    {
        _selectMoveUnit = selectMoveUnit;
    }

    public void MiniMapUnitMove(int clickMiniMapImageInstancePositionX, int clickMiniMapImageInstancePositionZ)
    {
        _selectMoveUnit.GetComponent<UnitMove>()
            .MiniMapClickUnitMove(clickMiniMapImageInstancePositionX, clickMiniMapImageInstancePositionZ);
    }
}