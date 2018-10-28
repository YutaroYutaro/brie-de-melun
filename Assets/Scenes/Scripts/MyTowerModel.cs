using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MyTowerModel : SingletonMonoBehaviour<MyTowerModel>
{
    public ReactiveProperty<int> TowerHitPointReactiveProperty = new IntReactiveProperty(3);
}