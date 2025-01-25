using TMPro;
using UnityEngine;

public class VelocityGameController : MonoBehaviour
{
    public float velocityGame = 1;
    private int level = 1;
    public float TimeBetweenLevels = 10f;
    public Player player;
    public TrapSpawner trapSpawner;
    public DOTRotateObject dotRotate;
    public GameObject VFXConfeti;
    public TextMeshProUGUI nivel;

    private void Start()
    {
        level = 1;
        UpdateTextLevel();
        InvokeRepeating(nameof(IncrementLevel), TimeBetweenLevels, TimeBetweenLevels);
    }

    private void IncrementLevel()
    {
        Debug.Log("Invoco incremento");
        if (level == 40)
        {
            CancelInvoke();
            return;
        }

        level++;
        UpdateTextLevel();
        InstantiateConfetiVFX();
        velocityGame *= 1.05f;
        player.ChangeVelocity(velocityGame);
        trapSpawner.SetVelocSpeedMultiplier(velocityGame);
        dotRotate.IncrementVelocity(velocityGame);
    }

    public void InstantiateConfetiVFX()
    {
        var extraLife_vfx = Instantiate(VFXConfeti, transform.position, Quaternion.identity);
        Destroy(extraLife_vfx, 1f);
    }

    private void UpdateTextLevel()
    {
        nivel.text = "Nivel " + level;
    }

}
