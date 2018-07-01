﻿using UnityEngine;

public class UnitStatus : MonoBehaviour {

	//操作ユニットの基本ステータス
    public float HitPoint = 0;
    public float AttackPoint = 0;
    public float DefensPoint = 0;
    public float MovementPoint = 0;

	// Use this for initialization
	void Start () {
		if (CompareTag("ProximityAttackUnit"))
		{
            Debug.Log("Proximity");
			this.HitPoint = 5;
			this.AttackPoint = 2;
			this.DefensPoint = 1;
			this.MovementPoint = 2;
        } 
		else if (CompareTag("RemoteAttackUnit")) 
		{
            Debug.Log("RemoteAttackUnit");
        }
		else
		{
			Debug.Log("ReconnaissanceUnit");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetUnitHitPoint()
	{
		return this.HitPoint;
	}
	
	public float GetUnitAttackPoint()
	{
		return this.AttackPoint;
	}
	
	public float GetUnitDefensPoint()
	{
		return this.DefensPoint;
	}
	
	public float GetUnitMovementPoint()
	{
		return this.MovementPoint;
	}
}
