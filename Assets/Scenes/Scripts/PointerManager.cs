using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : SingletonMonoBehaviour<PointerManager>
{

    [SerializeField] private bool _isDraged;

    public bool IsDraged
    {
        get => _isDraged;
        set => _isDraged = value;
    }
}
