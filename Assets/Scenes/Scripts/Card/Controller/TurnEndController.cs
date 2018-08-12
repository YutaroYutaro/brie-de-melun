using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndController : MonoBehaviour
{
    public void TurnEnd()
    {
        Debug.Log("Debug: TurnEnd");

//        List<GameObject> handcardList =
//            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetHandCardList();
//
//        foreach (GameObject handCard in handcardList)
//        {
//            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetGraveyardCardList()
//                .Add(handCard.gameObject);
//        }
//
//        handcardList.Clear();

        Transform handChildren = GameObject.Find("Hand").transform;
        List<GameObject> handCardObjects = new List<GameObject>();

        foreach (Transform handChild in handChildren)
        {
            handCardObjects.Add(handChild.gameObject);
            handChild.gameObject.SetActive(false);
        }

        foreach (GameObject cardObject in handCardObjects)
        {
            cardObject.transform.SetParent(GameObject.Find("Graveyard").transform);
        }
    }
}