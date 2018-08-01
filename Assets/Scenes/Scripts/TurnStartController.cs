using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartController : MonoBehaviour
{
    public int MaxTakenCards = 4;

    public void TurnStart()
    {
        Debug.Log("Debug: TurnStart");

//        List<GameObject> handcardList =
//            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetDeckCardList();
//
//        foreach (GameObject handCard in handcardList)
//        {
//            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetHandCardList()
//                .Add(handCard.gameObject);
//        }

        Transform deckChildren = GameObject.Find("Deck").transform;
        List<GameObject> deckCardObjects = new List<GameObject>();

        int takenCards = 0;

        foreach (Transform deckchild in deckChildren)
        {
            if (takenCards == MaxTakenCards)
                break;

            deckCardObjects.Add(deckchild.gameObject);
            Debug.Log(deckchild.gameObject.name);

            takenCards++;
        }

        Debug.Log(takenCards);

        if (takenCards != MaxTakenCards)
        {
            Transform graveyardChildren = GameObject.Find("Graveyard").transform;
            List<GameObject> graveCardObjects = new List<GameObject>();

            foreach (Transform graveyardChild in graveyardChildren)
            {
                graveCardObjects.Add(graveyardChild.gameObject);
            }

            foreach (var cardObject in graveCardObjects)
            {
                cardObject.transform.SetParent(GameObject.Find("Deck").transform);
            }

            foreach (GameObject deckchild in graveCardObjects)
            {
                if (takenCards == MaxTakenCards)
                    break;

                deckCardObjects.Add(deckchild);
                Debug.Log(deckchild.name);

                takenCards++;
            }
        }

//        List<GameObject> deckCardObjects =
//            GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>().GetHandCardList();

        foreach (var cardObject in deckCardObjects)
        {
            cardObject.transform.SetParent(GameObject.Find("Hand").transform);
            cardObject.gameObject.SetActive(true);
        }
    }
}