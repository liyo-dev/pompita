using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    //public int maxHealth = 6;
    private int currentHealth;

    void Start()
    {
        currentHealth = Utils.Variables.MaxHealth;
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > Utils.Variables.MaxHealth)
        {
            currentHealth = Utils.Variables.MaxHealth;
        }
        Debug.Log("Vida actual: " + currentHealth);
    }

    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }
        Debug.Log("Vida actual: " + currentHealth);
    }

    private void GameOver()
    {
        Debug.Log("Game Over: Explosion de pompa");
    }
}