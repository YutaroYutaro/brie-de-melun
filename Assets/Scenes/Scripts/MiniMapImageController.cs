using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private MiniMapImageInstancePosition _miniMapImageInstancePosition = null;

    private List<GameObject> _unitList = null;

    private string _nowPhase = null;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");

        _nowPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase();
        
        _miniMapImageInstancePosition = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>();

        if (_nowPhase == "SelectMoveUnit")
        {
            _unitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetUnitList();

            for (int i = 0; i < _unitList.Count; i++)
            {
                int unitPositionX = Mathf.RoundToInt(_unitList[i].transform.position.x);
                int unitPositionZ = Mathf.RoundToInt(_unitList[i].transform.position.z);

                if (unitPositionX == _miniMapImageInstancePosition.PosX &&
                    unitPositionZ == _miniMapImageInstancePosition.PosZ)
                {
                    Debug.Log(_unitList[i].gameObject.name);
                    GameObject.Find("UnitMoveManager").GetComponent<UnitMoveManager>().SetMoveUnit(_unitList[i]);
                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectDestination");
                }
            }   
        } 
        else if (_nowPhase == "SelectDestination")
        {
            GameObject.Find("UnitMoveManager").GetComponent<UnitMoveManager>().MiniMapUnitMove(_miniMapImageInstancePosition.PosX, _miniMapImageInstancePosition.PosZ);
            GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectUseCard");
            GameObject.Find("UnitMoveManager").GetComponent<UnitMoveManager>().SetMoveUnit(null);
            
        }

        //Debug.Log(this.gameObject.GetInstanceID());
        Debug.Log("PosX: " + _miniMapImageInstancePosition.PosX +
                  " PosZ: " + _miniMapImageInstancePosition.PosZ);
    }
    
    //マップオブジェクトにマウスがホバーしたときに色を変更
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<Image>().color = Color.red;
    }

    //ホバーが解除されたら元の色に戻す
    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<Image>().color = Color.white;
    }
}
