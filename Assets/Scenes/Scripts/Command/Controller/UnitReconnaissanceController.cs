using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class UnitReconnaissanceController : MonoBehaviour {

    public async void UnitReconnaissance()
    {
        int thisIntPosX = GetComponent<UnitOwnIntPosition>().PosX;
        int thisIntPosZ = GetComponent<UnitOwnIntPosition>().PosZ;

        GetComponent<UnitAnimator>().IsSearch = true;
        await Task.Delay(TimeSpan.FromSeconds(1.4f));
        GetComponent<UnitAnimator>().IsSearch = false;

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
