using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOfCard : MonoBehaviour
{
    [SerializeField]
    private int _mana;

    public int Mana => _mana;

    private void Start()
    {
        switch (gameObject.tag)
        {
                case "AttackCard":
                    _mana = 1;
                    break;
                case "MoveCard":
                    _mana = 1;
                    break;
                case "ReconnaissanceCard":
                    _mana = 1;
                    break;
                case "SummonCard":
                    _mana = 1;
                    break;
                default:
                    _mana = 1;
                    break;
        }
    }
}
