using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceOfCard : MonoBehaviour
{
    [SerializeField] private int _price;

    public int Price => _price;
}