using UniRx;

public class ManaModel : SingletonMonoBehaviour<ManaModel> {

    public ReactiveProperty<int> ManaReactiveProperty = new IntReactiveProperty(3);

}
