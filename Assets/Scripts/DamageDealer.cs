using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 1; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SubtractHealth(damageAmount);
                Destroy(gameObject);
            }
        }
    }
}