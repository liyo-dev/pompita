using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    //public int maxHealth = 6;
    private int currentHealth;
    [SerializeField] private TextMeshProUGUI vidasText;
    public UnityEvent OnPlayerDeath;
    public int currentScore = 0;
    
    
    void Start()
    {
        currentHealth = 1;
        UpdateHealthText();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > Utils.Variables.MaxHealth)
        {
            currentHealth = Utils.Variables.MaxHealth;
        }
        UpdateHealthText();
    }

    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }
        UpdateHealthText();
    }

    private void GameOver()
    {
        GoogleSheetsRanking.Instance.currentScore = currentScore;
        OnPlayerDeath.Invoke();
    }

    private void UpdateHealthText()
    {
        vidasText.text = "Vidas: " + currentHealth;
    }
}