using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlowManager : MonoBehaviour
{
//    private GameObject _deckText;
//    private GameObject _graveyardText;

    public List<GameObject> DeckCardList;
    public List<GameObject> GraveyardCardList;

    // Use this for initialization
    void Start()
    {
//        _deckText = GameObject.Find("Deck");
//        _graveyardText = GameObject.Find("Graveyard");
    }

    public List<GameObject> GetGraveyardCardList()
    {
        return GraveyardCardList;
    }

    public List<GameObject> GetDeckCardList()
    {
        return DeckCardList;
    }
}