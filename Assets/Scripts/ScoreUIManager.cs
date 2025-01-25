using System;
using TMPro;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + GoogleSheetsRanking.Instance.currentScore;
    }
}
