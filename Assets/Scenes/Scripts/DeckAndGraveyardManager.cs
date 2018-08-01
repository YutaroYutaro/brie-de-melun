using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckAndGraveyardManager : MonoBehaviour
{
    private List<GameObject> _deckCardList;
    private List<GameObject> _graveyardCardList;

    private CardFlowManager _cardFlowManager;

    private int _numberOfDeckCards;
    private int _numberOfGraveyardCards;

    private Text _deckText;
    private Text _graveyardText;

    // Use this for initialization
    void Start()
    {
        _cardFlowManager = GameObject.Find("CardFlowManager").GetComponent<CardFlowManager>();
//		_deckCardList = _cardFlowManager.GetDeckCardList();
//		_graveyardCardList = _cardFlowManager.GetGraveyardCardList();

        _numberOfDeckCards = _cardFlowManager.GetDeckCardList().Count;
        _numberOfGraveyardCards = _cardFlowManager.GetGraveyardCardList().Count;

        _deckText = GameObject.Find("Deck").GetComponent<Text>();
        _graveyardText = GameObject.Find("Graveyard").GetComponent<Text>();

        _deckText.text = _numberOfDeckCards.ToString();
        _graveyardText.text = _numberOfGraveyardCards.ToString();

//		Debug.Log(_deckText);
        Debug.Log(_numberOfGraveyardCards);
    }

    // Update is called once per frame
    void Update()
    {
        _deckCardList = _cardFlowManager.GetDeckCardList();
        _graveyardCardList = _cardFlowManager.GetGraveyardCardList();


        if (_numberOfDeckCards != _deckCardList.Count)
        {
            _deckText.text = _deckCardList.Count.ToString();
            _numberOfDeckCards = _deckCardList.Count;
        }

        if (_numberOfGraveyardCards != _graveyardCardList.Count)
        {
            Debug.Log(_graveyardCardList.Count);
            _graveyardText.text = _graveyardCardList.Count.ToString();
            _numberOfGraveyardCards = _graveyardCardList.Count;
        }
    }
}