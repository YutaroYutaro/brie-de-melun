using UnityEngine;
using UnityEngine.UI;

public class DeckAndGraveyardManager : MonoBehaviour
{
    private int _numberOfDeckCards;
    private int _numberOfGraveyardCards;

    private Text _deckText;
    private Text _graveyardText;


    void Start()
    {
        _deckText = GameObject.Find("Deck").GetComponent<Text>();
        _graveyardText = GameObject.Find("Graveyard").GetComponent<Text>();

        _deckText.text = GameObject.Find("Deck").transform.childCount.ToString();
        _graveyardText.text = GameObject.Find("Graveyard").transform.childCount.ToString();
    }

    void Update()
    {
        if (_numberOfDeckCards != GameObject.Find("Deck").transform.childCount)
        {
            _deckText.text = GameObject.Find("Deck").transform.childCount.ToString();
            _numberOfDeckCards = GameObject.Find("Deck").transform.childCount;
        }

        if (_numberOfGraveyardCards != GameObject.Find("Graveyard").transform.childCount)
        {
            _graveyardText.text = GameObject.Find("Graveyard").transform.childCount.ToString();
            _numberOfGraveyardCards = GameObject.Find("Graveyard").transform.childCount;
        }
    }
}