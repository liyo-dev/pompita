using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager Instance { get; private set; }
    
    private TextMeshProUGUI scoreText;
    
    private VelocityGameController velocityGameController;

    public UnityEvent OnFastmode;

    private void Start()
    {
        velocityGameController = GameObject.FindObjectOfType<VelocityGameController>();
        velocityGameController.OnIncrementLevel += SetFastMode;
        UpdateScoreTextReference();
    }

    private void SetFastMode()
    {
        OnFastmode.Invoke();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.UpdateScoreTextReference();
            Destroy(gameObject);
        }
    }

    public void UpdateUI()
    {
        scoreText.text = "" + GoogleSheetsRanking.Instance.currentScore;
    }

    public void UpdateScoreTextReference()
    {
        scoreText = GameObject.FindGameObjectWithTag("TextScore").GetComponent<TextMeshProUGUI>();
        GoogleSheetsRanking.Instance.currentScore = 0;
        UpdateUI();
    }
}
