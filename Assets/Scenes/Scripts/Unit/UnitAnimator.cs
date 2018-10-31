using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isAttack;
    [SerializeField] private bool _isMiddleAttack;
    [SerializeField] private bool _isLongAttack;
    [SerializeField] private bool _isSearch;
    [SerializeField] private bool _isMove;
    [SerializeField] private bool _isDamaged;
    [SerializeField] private bool _isDefeated;

    public bool IsMove
    {
        set => _isMove = value;
    }

    public bool IsAttack
    {
        set => _isAttack = value;
    }

    public bool IsMiddleAttack
    {
        get => _isMiddleAttack;
        set => _isMiddleAttack = value;
    }

    public bool IsLongAttack
    {
        get => _isLongAttack;
        set => _isLongAttack = value;
    }

    public bool IsDamaged
    {
        set => _isDamaged = value;
    }


    public bool IsDefeated
    {
        set => _isDefeated = value;
    }

    public bool IsSearch
    {
        set => _isSearch = value;
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

        if (!CompareTag("ReconnaissanceUnit"))
        {
            if (_isAttack)
            {
                _animator.SetBool("Attack", _isAttack);
            }
            else
            {
                _animator.SetBool("Attack", _isAttack);
            }

            if (_isMiddleAttack)
            {
                _animator.SetBool("MiddleAttack", _isMiddleAttack);
            }
            else
            {
                _animator.SetBool("MiddleAttack", _isMiddleAttack);
            }

            if (_isLongAttack)
            {
                _animator.SetBool("LongAttack", _isLongAttack);
            }
            else
            {
                _animator.SetBool("LongAttack", _isLongAttack);
            }
        }
        else
        {
            if (_isSearch)
            {
                _animator.SetBool("Search", _isSearch);
            }
            else
            {
                _animator.SetBool("Search", _isSearch);
            }
        }

        if (_isDamaged)
        {
            _animator.SetBool("Damaged", _isDamaged);
        }
        else
        {
            _animator.SetBool("Damaged", _isDamaged);
        }

        if (_isDefeated)
        {
            _animator.SetBool("Defeated", _isDefeated);
        }
        else
        {
            _animator.SetBool("Defeated", _isDefeated);
        }
    }
}