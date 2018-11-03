using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndController : MonoBehaviour
{
    public void TurnEnd()
    {
        if (PhaseManager.Instance.PhaseReactiveProperty.Value == "EnemyTurn") return;
        Debug.Log("Debug: TurnEnd");

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

        PhaseManager.Instance.SetNextPhase("EnemyTurn");
        EnemyUnitController.Instance.TurnStart();
    }
}