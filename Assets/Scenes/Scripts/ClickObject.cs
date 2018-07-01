using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject{

    public static GameObject GetClickObject()
    {
        GameObject result = null;
        // 左クリックされた場所のオブジェクトを取得
		
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            result = hit.collider.gameObject;
        }
		
        return result;
    }
}