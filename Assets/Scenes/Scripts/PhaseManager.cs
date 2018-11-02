using UnityEngine;
using UniRx;

public class PhaseManager : SingletonMonoBehaviour<PhaseManager>
{
//    public string StartPhase = "SelectUseCard";

	[SerializeField]
	private string _nowPhase;
	public ReactiveProperty<string> PhaseReactiveProperty;

	protected override void Init(){
		base.Init ();
		PhaseReactiveProperty = new StringReactiveProperty("EnemyTurn");
		_nowPhase = "EnemyTurn";
	}

	public void SetNextPhase(string phase)
	{
		PhaseReactiveProperty.Value = phase;
		_nowPhase = phase;
	}

	public string GetNowPhase()
	{
		return _nowPhase;
	}
}
