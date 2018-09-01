using UnityEngine;
using System.Collections.Generic;

public class UnitStatus : MonoBehaviour
{
    //操作ユニットの基本ステータス
    public int HitPoint;
    public int AttackPoint;
    public int DefensPoint;
    public int MovementPoint;

    void Start()
    {
        if (CompareTag("ProximityAttackUnit"))
        {
            HitPoint = 5;
            AttackPoint = 2;
            DefensPoint = 1;
            MovementPoint = 2;
        }
        else if (CompareTag("RemoteAttackUnit"))
        {
            HitPoint = 3;
            AttackPoint = 3;
            DefensPoint = 1;
            MovementPoint = 2;
        }
        else
        {
            HitPoint = 3;
            AttackPoint = 0;
            DefensPoint = 0;
            MovementPoint = 2;
        }
    }

    void Update()
    {
        if (HitPoint <= 0)
        {
            Transform player1UnitChildren = GameObject.Find("Player1Units").transform;

            foreach (Transform player1UnitChild in player1UnitChildren)
            {
                if (player1UnitChild.gameObject == gameObject)
                {
                    Destroy(player1UnitChild.gameObject);
                    return;
                }
            }

            Transform player2UnitChildren = GameObject.Find("Player2Units").transform;

            foreach (Transform player2UnitChild in player2UnitChildren)
            {
                if (player2UnitChild.gameObject == gameObject)
                {
                    Destroy(player2UnitChild.gameObject);
                    return;
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
        HitPoint = unitStatus.HitPoint;
    }
}