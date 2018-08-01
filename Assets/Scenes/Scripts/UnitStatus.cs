using UnityEngine;
using System.Collections.Generic;

public class UnitStatus : MonoBehaviour
{
    //操作ユニットの基本ステータス
    public float HitPoint = 0;
    public float AttackPoint = 0;
    public float DefensPoint = 0;
    public float MovementPoint = 0;

    private List<GameObject> _unitList;
    private List<GameObject> _enemyUnitList;

    // Use this for initialization
    void Start()
    {
        _unitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetMyUnitList();
        _enemyUnitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetEnemyUnitList();

        if (CompareTag("ProximityAttackUnit"))
        {
            //Debug.Log("Proximity");
            this.HitPoint = 5;
            this.AttackPoint = 2;
            this.DefensPoint = 1;
            this.MovementPoint = 2;
        }
        else if (CompareTag("RemoteAttackUnit"))
        {
            //Debug.Log("RemoteAttackUnit");
        }
        else
        {
            //Debug.Log("ReconnaissanceUnit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.HitPoint <= 0)
        {
            for (int i = 0; i < _unitList.Count; i++)
            {
                if (_unitList[i] == this.gameObject)
                {
                    Destroy(_unitList[i]);
                    _unitList.RemoveAt(i);
                    break;
                }
            }

            foreach (var enemyUnit in _enemyUnitList)
            {
                if (enemyUnit == this.gameObject)
                {
                    Destroy(enemyUnit);
                    _enemyUnitList.Remove(enemyUnit);
                    break;
                }
            }
        }
    }

    public UnitStatus GetUnitStatus()
    {
        return this;
    }

    public void SetUnitStatus(UnitStatus unitStatus)
    {
        this.HitPoint = unitStatus.HitPoint;
        //Debug.Log("targetHitPoint : " + this.HitPoint);
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