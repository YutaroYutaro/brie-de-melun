using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModel : SingletonMonoBehaviour<HandModel>
{
    public void GenerateCard(GameObject cardPrefab)
    {
        GameObject card = Instantiate(cardPrefab);

        card.transform.localScale = transform.lossyScale;
        card.transform.position = transform.position;
        card.transform.rotation = transform.rotation;

        card.transform.SetParent(transform);
    }
}