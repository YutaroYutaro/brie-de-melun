using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReconnaissanceController : MonoBehaviour {

    public void UnitReconnaissance()
    {
        int thisIntPosX = GetComponent<UnitOwnIntPosition>().PosX;
        int thisIntPosZ = GetComponent<UnitOwnIntPosition>().PosZ;

        GameObject.Find("FogManager").GetComponent<FogManager>().ClearFog(thisIntPosX + 1, thisIntPosZ);
        GameObject.Find("FogManager").GetComponent<FogManager>().ClearFog(thisIntPosX - 1, thisIntPosZ);
        GameObject.Find("FogManager").GetComponent<FogManager>().ClearFog(thisIntPosX, thisIntPosZ + 1);
        GameObject.Find("FogManager").GetComponent<FogManager>().ClearFog(thisIntPosX, thisIntPosZ - 1);

        GameObject.Find("FogManager").GetComponent<FogManager>().SetActiveUnitInFog(thisIntPosX + 1, thisIntPosZ);
        GameObject.Find("FogManager").GetComponent<FogManager>().SetActiveUnitInFog(thisIntPosX - 1, thisIntPosZ);
        GameObject.Find("FogManager").GetComponent<FogManager>().SetActiveUnitInFog(thisIntPosX, thisIntPosZ + 1);
        GameObject.Find("FogManager").GetComponent<FogManager>().SetActiveUnitInFog(thisIntPosX, thisIntPosZ - 1);
    }
}
