﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvas : MonoBehaviour {

    void Awake()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
