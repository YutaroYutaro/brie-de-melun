using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Scenes.Scripts.Command.Presenter
{
    public class ShopPresenter : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        void Start()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => ShopCanvas.Instance.ShopCanvasClose());
        }
    }
}