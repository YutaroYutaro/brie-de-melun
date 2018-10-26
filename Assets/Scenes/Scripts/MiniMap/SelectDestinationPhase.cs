using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.MiniMap
{
    public class SelectDestinationPhase : IPhaseType
    {
        public async void PhaseController(int posX, int posZ)
        {
            if(UnitMoveManager.Instance.IsMoving == true) return;

            UnitMoveManager.Instance.IsMoving = true;

            if (await UnitMoveManager.Instance.MiniMapUnitMove(posX, posZ))
            {
                UnitMoveManager.Instance.IsMoving = false;

                if (UnitMoveManager.Instance.SelectedUnitMovementPoint > 0) return;

                Debug.Log("End Move!");

                PhaseManager.Instance.SetNextPhase("SelectUseCard");

                Debug.Log("Phase: SelectUseCard");

                UnitMoveManager.Instance.SetMoveUnit(null);
            }
            else
            {
                Debug.Log("One more select destination.");
                UnitMoveManager.Instance.IsMoving = false;
            }
        }
    }
}