using UnityEngine;

public class ScorePoints : MonoBehaviour
{   
    public int scoreAmount = 1;
    public void AddScore(int amount)
    {
        GoogleSheetsRanking.Instance.currentScore += amount;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                AddScore(scoreAmount);
                Destroy(gameObject);
            }
        }
    }
}
