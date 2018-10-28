using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class MapObjectController : MonoBehaviour
{
    public GameObject[] MapObjectType;

    public GameObject FogPrefab;

    private GameObject _fogGameObject;

    public void InstantiateMapObject()
    {
        Instantiate(
            MapObjectType[
                CreateMap.Instance.GetMapObjectTypeTable(
                    Mathf.RoundToInt(transform.position.x),
                    Mathf.RoundToInt(transform.position.z)
                )
            ],
            new Vector3(
                Mathf.RoundToInt(transform.position.x),
                0,
                Mathf.RoundToInt(transform.position.z)
            ),
            Quaternion.identity
        );
    }

    public void InstantiateFog()
    {
        _fogGameObject = Instantiate(
            FogPrefab,
            new Vector3(
                Mathf.RoundToInt(transform.position.x),
                1,
                Mathf.RoundToInt(transform.position.z)
            ),
            Quaternion.identity
        );
    }

    public void FogDestroy()
    {
        Destroy(_fogGameObject);
    }
}