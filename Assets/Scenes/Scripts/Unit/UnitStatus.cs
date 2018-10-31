using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UnitStatus : MonoBehaviour
{
    //操作ユニットの基本ステータス
    public int HitPoint;
    public int AttackPoint;
    public int DefensPoint;
    public int MovementPoint;

    private int _defaultAttackPoint;

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

        _defaultAttackPoint = AttackPoint;

        StartCoroutine(Defeated());
    }

    public void MapObjectEffect(int posX, int posZ)
    {
        switch (CreateMap.Instance.GetMapObjectTypeTable(posX, posZ))
        {
            case 0:
                AttackPoint = _defaultAttackPoint;
                break;
            case 1:
                AttackPoint += 2;
                break;
            case 2:
                AttackPoint = _defaultAttackPoint;
                break;
            case 3:
                break;
            default:
                break;
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

    // コルーチン
    private IEnumerator Defeated()
    {
        // コルーチンの処理

        while (true)
        {
            if (HitPoint <= 0)
            {
                GetComponent<UnitAnimator>().IsDefeated = true;
                yield return new WaitForSeconds(1.4f);
                Destroy(gameObject);
//                Transform player1UnitChildren = GameObject.Find("Player1Units").transform;
//
//                foreach (Transform player1UnitChild in player1UnitChildren)
//                {
//                    if (player1UnitChild.gameObject == gameObject)
//                    {
//                        GetComponent<UnitAnimator>().IsDefeated = true;
//                        yield return new WaitForSeconds (1.4f);
//                        Destroy(player1UnitChild.gameObject);
//                    }
//                }
//
//                Transform player2UnitChildren = GameObject.Find("Player2Units").transform;
//
//                foreach (Transform player2UnitChild in player2UnitChildren)
//                {
//                    if (player2UnitChild.gameObject == gameObject)
//                    {
//                        GetComponent<UnitAnimator>().IsDefeated = true;
//                        yield return new WaitForSeconds (1.4f);
//                        Destroy(player2UnitChild.gameObject);
//                    }
//                }
            }

            yield return null;
        }
    }
}