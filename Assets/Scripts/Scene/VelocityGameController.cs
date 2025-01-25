using UnityEngine;

public class VelocityGameController : MonoBehaviour
{
    public float velocityGame = 1;
    private int level = 1;
    public float TimeBetweenLevels = 5f;
    public Player player;
    public TrapSpawner trapSpawner;
    public DOTRotateObject dotRotate;

    private void Start()
    {
        level = 1;
        InvokeRepeating(nameof(IncrementLevel), TimeBetweenLevels, TimeBetweenLevels);
    }

    private void IncrementLevel()
    {
        level++;
        velocityGame *= 1.05f;
        player.ChangeVelocity(velocityGame);
        trapSpawner.SetVelocSpeedMultiplier(velocityGame);
        dotRotate.IncrementVelocity(velocityGame);
    }

}
