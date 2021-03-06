﻿using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnStartController : MonoBehaviour
{
    public int MaxTakenCards = 4;

    private void Start()
    {
        PhaseManager.Instance.PhaseReactiveProperty
                .Zip(PhaseManager.Instance.PhaseReactiveProperty.Skip(1), (x,y)
                => new Tuple<string,string>(x, y))
            .Where(phase => phase.Item1 == "EnemyTurn" && phase.Item2 == "SelectUseCard")
            .Subscribe(_ =>
                {
                    TurnStart();
                }
            );
    }

    public void TurnStart()
    {
        Debug.Log("Debug: TurnStart");

        ManaModel.Instance.ManaReactiveProperty.Value = 3;

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

            for (int i = 0; i < graveCardObjects.Count; i++)
            {
                GameObject temp = graveCardObjects[i];
                int randomIndex = Random.Range(0, graveCardObjects.Count);
                graveCardObjects[i] = graveCardObjects[randomIndex];
                graveCardObjects[randomIndex] = temp;
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

        PhaseManager.Instance.SetNextPhase("SelectUseCard");
    }
}