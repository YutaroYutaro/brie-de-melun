using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isAttack = false;
    [SerializeField] private bool _isMove = false;
    [SerializeField] private bool _isDamaged = false;
    [SerializeField] private bool _isDefeated = false;

    public bool IsMove
    {
        get => _isMove;
        set => _isMove = value;
    }

    public bool IsAttack
    {
        get => _isAttack;
        set => _isAttack = value;
    }

    public bool IsDamaged
    {
        get => _isDamaged;
        set => _isDamaged = value;
    }


    public bool IsDefeated
    {
        get => _isDefeated;
        set => _isDefeated = value;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
        {
            _animator.SetBool("Move", _isMove);
        }
        else
        {
            _animator.SetBool("Move", _isMove);
        }

        if (_isAttack)
        {
            _animator.SetBool("Attack", _isAttack);
        }
        else
        {
            _animator.SetBool("Attack", _isAttack);
        }

        if (_isDamaged)
        {
            Debug.Log("Here!");
            _animator.SetBool("Damaged", _isDamaged);
        }
        else
        {
            _animator.SetBool("Damaged", _isDamaged);
        }

        if (_isDefeated)
        {
            Debug.Log("Here!");
            _animator.SetBool("Defeated", _isDefeated);
        }
        else
        {
            _animator.SetBool("Defeated", _isDefeated);
        }
    }
}