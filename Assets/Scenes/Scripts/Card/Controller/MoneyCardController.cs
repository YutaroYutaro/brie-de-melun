using UnityEngine;

namespace Asset.Scripts.Cards
{
    public class MoneyCardController : ICardType
    {
        public bool SetCardTypePhase()
        {
            MoneyModel.Instance.MoneyReactiveProperty.Value += 2;
            return true;
        }
    }
}