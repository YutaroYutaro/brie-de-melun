using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
	private GameObject _unitAttackManager;

	private UnitAttackManager _attackCommand;

	private GameObject _clickObject;
	
	// Use this for initialization
	void Start () {
		_unitAttackManager = GameObject.Find("UnitAttackManager");

		_attackCommand = _unitAttackManager.GetComponent<UnitAttackManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_clickObject = ClickObject.GetClickObject();

			if (this.ExistTarget(Mathf.RoundToInt(_clickObject.transform.position.x),
				Mathf.RoundToInt(_clickObject.transform.position.z)))
			{

				if (_clickObject.CompareTag("ProximityAttackUnit"))
				{
					Debug.Log("ProximityAttackUnit");
					_attackCommand.AttackCommand(this.gameObject, _clickObject);
				}
				else if (_clickObject.CompareTag("RemoteAttackUnit"))
				{
					Debug.Log("RemoteAttackUnit");
				}
				else if (_clickObject.CompareTag("ReconnaissanceUnit"))
				{
					Debug.Log("ReconnaissanceUnit");
				}
			}
		}
	}

	private bool ExistTarget(int targetPositionX, int targetPositionZ)
	{
		float absX = Math.Abs(this.gameObject.transform.position.x - targetPositionX);
		float absZ = Math.Abs(this.gameObject.transform.position.z - targetPositionZ);
		
		if (absX <= 1 && absZ <= 1)
		{
			return true;
		}
		
		return false;
	}
}
