using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndController : MonoBehaviour
{
    public void TurnEnd()
    {
        Debug.Log("Debug: TurnEnd");

        Transform handChildren = GameObject.Find("Hand").transform;

        Transform[] children;

        foreach (Transform handChild in handChildren)
        {
            Debug.Log(handChild.name);

            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetGraveyardCardList()
                .Add(handChild.gameObject);
            handChild.gameObject.SetActive(false);

            Debug.Log(handChild.name);
        }

        List<GameObject> handCard =
            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetGraveyardCardList();

        foreach (var card in handCard)
        {
            card.transform.SetParent(GameObject.Find("Graveyard").transform);
        }
    }
}