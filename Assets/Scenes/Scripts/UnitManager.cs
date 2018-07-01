using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	public List<GameObject> UnitList;

	public GameObject CreatedUnit;
	
	// Use this for initialization
	void Start () {
		//UnitList = new List<GameObject>();
		
		GameObject toggleInstance = Instantiate(CreatedUnit) as GameObject;
		
		UnitList.Add(toggleInstance);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<GameObject> GetUnitList()
	{
		return UnitList;
	}
}
