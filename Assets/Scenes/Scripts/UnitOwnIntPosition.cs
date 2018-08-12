using UnityEngine;

public class UnitOwnIntPosition : MonoBehaviour
{
    public int PosX = 0;
    public int PosZ = 0;

    public void SetUnitOwnIntPosition(int currentUnitPosX, int currentUnitPosZ)
    {
        PosX = currentUnitPosX;
        PosZ = currentUnitPosZ;
    }
}