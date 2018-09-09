using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class ManaModel : MonoBehaviour {

    public ReactiveProperty<int> ManaReactiveProperty = new IntReactiveProperty(3);

}
