using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<GameObject> MyUnitList;
    public List<GameObject> EnemyUnitList;

    public GameObject CreatedUnit;

    void Start()
    {
        //GameObject toggleInstance = Instantiate(CreatedUnit) as GameObject;

//        MyUnitList.Add(toggleInstance);

//        toggleInstance.transform.SetParent(GameObject.Find("Player1Units").transform);
    }

    public List<GameObject> GetMyUnitList()
    {
        return MyUnitList;
    }

    public List<GameObject> GetEnemyUnitList()
    {
        return EnemyUnitList;
    }
}