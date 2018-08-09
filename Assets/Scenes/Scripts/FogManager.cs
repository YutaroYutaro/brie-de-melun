using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public void ClearFog(int mapObjectPosX, int mapObjectPosZ)
    {
        Transform foggyMapObjectsChildren = GameObject.Find("FoggyMapObjects").transform;

        foreach (Transform foggyMapObjectsChild in foggyMapObjectsChildren)
        {
            if (mapObjectPosX == Mathf.RoundToInt(foggyMapObjectsChild.position.x) &&
                (mapObjectPosZ == Mathf.RoundToInt(foggyMapObjectsChild.position.z)))
            {
                foggyMapObjectsChild.transform.SetParent(GameObject.Find("ClearMapObjects").transform);
            }
        }
    }

    public GameObject SetActiveUnitInFog(int unitPosX, int unitPosZ)
    {
        Transform player2UnitsChildren = GameObject.Find("Player2Units").transform;

        foreach (Transform player2UnitsChild in player2UnitsChildren)
        {
            if (unitPosX == Mathf.RoundToInt(player2UnitsChild.position.x) &&
                unitPosZ == Mathf.RoundToInt(player2UnitsChild.position.z) &&
                !(player2UnitsChild.gameObject.activeSelf))
            {
                player2UnitsChild.gameObject.SetActive(true);
                return player2UnitsChild.gameObject;
            }
        }

        return null;
    }
}