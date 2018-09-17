using UniRx;

    public class MoneyModel : SingletonMonoBehaviour<MoneyModel>
    {
        public ReactiveProperty<int> MoneyReactiveProperty = new IntReactiveProperty(0);
    }
