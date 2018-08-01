using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public string StartPhase = "SelectUseCard";

    private string _nowPhase = null;

    private void Start()
    {
        _nowPhase = StartPhase;
    }

    public void SetNextPhase(string phase)
    {
        _nowPhase = phase;
    }

    public string GetNowPhase()
    {
        return _nowPhase;
    }
}
