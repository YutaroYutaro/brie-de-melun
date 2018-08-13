using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public int[,] PlayerOneFogMapState = new int[5, 7];
    public int[,] PlayerTwoFogMapState = new int[5, 7];

    public Material[] MaterialTypeList;

    public void ClearFog(int mapObjectPosX, int mapObjectPosZ)
    {
        Transform foggyMapObjectsChildren = GameObject.Find("FoggyMapObjects").transform;

        foreach (Transform foggyMapObjectsChild in foggyMapObjectsChildren)
        {
            if (mapObjectPosX == Mathf.RoundToInt(foggyMapObjectsChild.position.x) &&
                (mapObjectPosZ == Mathf.RoundToInt(foggyMapObjectsChild.position.z)))
            {
                foggyMapObjectsChild.transform.SetParent(GameObject.Find("ClearMapObjects").transform);
                foggyMapObjectsChild.GetComponent<Renderer>().material.color = MaterialTypeList[
                    GameObject.Find("Map").GetComponent<CreateMap>().GetMapObjectTypeTable(
                        Mathf.RoundToInt(foggyMapObjectsChild.position.x),
                        Mathf.RoundToInt(foggyMapObjectsChild.position.z)
                    )
                ].color;
            }
        }
    }

    public GameObject SetActiveUnitInFog(int unitPosX, int unitPosZ)
    {
        Transform player2UnitsChildren = GameObject.Find("Player2Units").transform;

        foreach (Transform player2UnitsChild in player2UnitsChildren)
        {
            if (unitPosX == player2UnitsChild.GetComponent<UnitOwnIntPosition>().PosX &&
                unitPosX == player2UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ &&
                !(player2UnitsChild.gameObject.activeSelf))
            {
                player2UnitsChild.gameObject.SetActive(true);
                return player2UnitsChild.gameObject;
            }
        }

        return null;
    }

    public int[,] GetPlayerOneFogMapState()
    {
        return PlayerOneFogMapState;
    }

    public void SetPlayerOneFogMapState(int posX, int posZ, int fogFlag)
    {
        PlayerOneFogMapState[posX, posZ] = fogFlag;
    }

    public int[,] GetPlayerTwoFogMapState()
    {
        return PlayerTwoFogMapState;
    }

    public void SetPlayerTwoFogMapState(int posX, int posZ, int fogFlag)
    {
        PlayerTwoFogMapState[posX, posZ] = fogFlag;
    }
}