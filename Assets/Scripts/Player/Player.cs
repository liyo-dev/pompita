using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 6;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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