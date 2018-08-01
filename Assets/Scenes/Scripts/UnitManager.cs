using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<GameObject> MyUnitList;
    public List<GameObject> EnemyUnitList;

    public GameObject CreatedUnit;

    // Use this for initialization
    void Start()
    {
        //UnitList = new List<GameObject>();

        GameObject toggleInstance = Instantiate(CreatedUnit) as GameObject;

        MyUnitList.Add(toggleInstance);
    }

    // Update is called once per frame
    void Update()
    {
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