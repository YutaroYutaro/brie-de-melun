using UniRx;

public class TowerModel : SingletonMonoBehaviour<TowerModel>
{
    public ReactiveProperty<int> TowerHitPointReactiveProperty = new IntReactiveProperty(3);
}