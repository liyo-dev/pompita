using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public int maxHealth = 6;
    private int currentHealth;
    [SerializeField] private TextMeshProUGUI vidasText;
    public UnityEvent OnPlayerDeath;

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
        OnPlayerDeath.Invoke();
    }

    private void UpdateHealthText()
    {
        vidasText.text = "Vidas: " + currentHealth;
    }
}