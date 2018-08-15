using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private string _nowPhase;

    private void Start()
    {
        _nowPhase = "SelectUseCard";
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
