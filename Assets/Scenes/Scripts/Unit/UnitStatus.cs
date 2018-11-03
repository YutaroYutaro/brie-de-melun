using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using UniRx;

public class UnitStatus : MonoBehaviour
{
    //操作ユニットの基本ステータス
    public int HitPoint;
    public int AttackPoint;
    public int DefensPoint;
    public int MovementPoint;

    private int _defaultAttackPoint;

    [SerializeField] private bool _onGoldMine;
    [SerializeField] private bool _isDead;

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

        MapObjectEffect(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "EnemyTurn" && _onGoldMine
            )
            .Subscribe(_ => MoneyModel.Instance.MoneyReactiveProperty.Value += 1);

//        StartCoroutine(Defeated());
    }

    public void MapObjectEffect(int posX, int posZ)
    {
        switch (CreateMap.Instance.GetMapObjectTypeTable(posX, posZ))
        {
            case 0:
                AttackPoint = _defaultAttackPoint;
                _onGoldMine = false;
                break;
            case 1:
                AttackPoint = 2 + _defaultAttackPoint;
                _onGoldMine = false;
                break;
            case 2:
                AttackPoint = _defaultAttackPoint;
                _onGoldMine = true;
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

//    // コルーチン
//    private IEnumerator Defeated()
//    {
//        // コルーチンの処理
//
//        while (true)
//        {
//            if (HitPoint <= 0)
//            {
//                GetComponent<UnitAnimator>().IsDefeated = true;
//                yield return new WaitForSeconds(1.4f);
//                Destroy(gameObject);
//            }
//
//            yield return null;
//        }
//    }

    private async void Update()
    {
        if (HitPoint <= 0 && !_isDead)
        {
            _isDead = true;
            GetComponent<UnitAnimator>().IsDefeated = true;
            await Task.Delay(TimeSpan.FromSeconds(1.4f));
            Destroy(gameObject);
        }
    }
}