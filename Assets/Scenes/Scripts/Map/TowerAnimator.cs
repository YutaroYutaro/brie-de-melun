using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void FirstDestroy()
    {
        _animator.SetBool("Destroyed1st", true);
    }

    public void SecondDestroy()
    {
        _animator.SetBool("Destroyed2nd", true);
    }

    public void ThirdDestroy()
    {
        _animator.SetBool("Destroyed3rd", true);
    }
}