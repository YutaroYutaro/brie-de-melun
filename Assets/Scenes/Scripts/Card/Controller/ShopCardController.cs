using UnityEngine;

namespace Asset.Scripts.Cards
{
    public class ShopCardController : ICardType
    {

        public bool SetCardTypePhase()
        {
            ShopCanvas.Instance.ShopCanvasOpen();
            return true;
        }
    }
}