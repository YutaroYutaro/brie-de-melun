using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRotationController : MonoBehaviour
{
    public void UnitRotation(int differenceX, int differenceZ)
    {
//        Debug.Log(differenceX + ", " + differenceZ);
        if (differenceX == 1 && differenceZ == 1)
        {
            transform.eulerAngles = new Vector3(0, 45f, 0);
        }
        else if (differenceX == -1 && differenceZ == 1)
        {
            transform.eulerAngles = new Vector3(0, 315f, 0);
        }
        else if (differenceX == 1 && differenceZ == -1)
        {
            transform.eulerAngles = new Vector3(0, 135f, 0);
        }
        else if (differenceX == -1 && differenceZ == -1)
        {
            transform.eulerAngles = new Vector3(0, 225f, 0);
        }
        else if (differenceZ == 1 || differenceZ == 2)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (differenceX == 1 || differenceX == 2)
        {
            transform.eulerAngles = new Vector3(0, 90f, 0);
        }
        else if (differenceZ == -1 || differenceZ == -2)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else if (differenceX == -1 || differenceX == -2)
        {
            transform.eulerAngles = new Vector3(0, 270f, 0);
        }
    }
}