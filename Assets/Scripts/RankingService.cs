using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RankingService : MonoBehaviour
{
    private static GoogleSheetsRanking rankingInstance;
    public UnityEvent OnRankingDone;
    [SerializeField] private TextMeshProUGUI top5String;

    public void Start()
    {
        rankingInstance = GameObject.FindObjectOfType<GoogleSheetsRanking>();
        if (rankingInstance == null)
        {
            Debug.LogError("GoogleSheetsRanking instance not found in the scene.");
        }

        rankingInstance.OnRankingUpdated += OnRankingUpdated;
    }

    private void OnRankingUpdated()
    {
        var top5List = rankingInstance.GetTop5String();
        top5String.text = string.Join("\n", top5List);
        OnRankingDone.Invoke();
    }

    public void GetRanking()
    {
        rankingInstance.ObtenerRanking();
    }

    public void SendScore(string name, int score)
    {
        rankingInstance.EnviarPuntuacion(name, score);
    }

    public void Reset()
    {
        rankingInstance.Reset();
    }

    public void GetTop5()
    {
        rankingInstance.GetTop5();
    }
}