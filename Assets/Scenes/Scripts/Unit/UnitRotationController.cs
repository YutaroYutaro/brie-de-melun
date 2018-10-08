using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRotationController : MonoBehaviour
{
    public void UnitRotation(int differenceX, int differenceZ)
    {
//        Debug.Log(differenceX + ", " + differenceZ);

        if (differenceZ == 1)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
        else if (differenceX == 1)
        {
            transform.eulerAngles = new Vector3 (0, 90f, 0);
        }
        else if (differenceZ == -1)
        {
            transform.eulerAngles = new Vector3 (0, 180f, 0);
        }
        else if (differenceX == -1)
        {
            transform.eulerAngles = new Vector3 (0, 270f, 0);
        }
    }
}