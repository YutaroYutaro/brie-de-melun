using System.Collections.Generic;
using UnityEngine;

public class TurnStartController : MonoBehaviour
{
    public int MaxTakenCards = 4;

    public void TurnStart()
    {
        Debug.Log("Debug: TurnStart");

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

        foreach (var cardObject in deckCardObjects)
        {
            cardObject.transform.SetParent(GameObject.Find("Hand").transform);
            cardObject.gameObject.SetActive(true);
        }
    }
}