using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewId : MonoBehaviour
{
    [SerializeField]private int _unitViewId;

    public int UnitViewId
    {
        get => _unitViewId;
        set => _unitViewId = value;
    }
}