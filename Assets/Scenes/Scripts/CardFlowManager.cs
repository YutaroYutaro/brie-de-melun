using System.Collections.Generic;
using UnityEngine;

public class CardFlowManager : MonoBehaviour
{
    public List<GameObject> DeckCardList;
    public List<GameObject> GraveyardCardList;

    public List<GameObject> GetGraveyardCardList()
    {
        return GraveyardCardList;
    }

    public List<GameObject> GetDeckCardList()
    {
        return DeckCardList;
    }
}