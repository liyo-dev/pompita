using UnityEngine;

public class LifeDealer : MonoBehaviour
{
    public int lifeAmount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.InstantiateExtraLifeVFX();
                player.AddHealth(lifeAmount);
                Destroy(gameObject);
            }
        }
    }
}
