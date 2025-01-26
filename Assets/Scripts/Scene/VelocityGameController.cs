using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class VelocityGameController : MonoBehaviour
{
    public float velocityGame = 1;
    private int level = 1;
    public int Level => level;
    public float TimeBetweenLevels = 10f;
    public Player player;
    public TrapSpawner trapSpawner;
    public DOTRotateObject dotRotate;
    public GameObject VFXConfeti;
    public TextMeshProUGUI nivel;
    public UnityAction OnIncrementLevel;
    public UnityAction OnModoMaximo;

    private void Awake()
    {
        nivel.gameObject.SetActive(false);
    }

    private void Start()
    {
        level = 1;
        UpdateTextLevel();
        InvokeRepeating(nameof(IncrementLevel), TimeBetweenLevels, TimeBetweenLevels);
        player.OnPlayerDeathAction += FinishGame;
    }

    private void IncrementLevel()
    {
        if (level == 20)
        {
            OnModoMaximo?.Invoke();
            AudioManager.Instance.PlayEpic();
        }
        
        if (level == 40)
        {
            CancelInvoke();
            return;
        }

        level++;
        OnIncrementLevel?.Invoke();
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
        AnimateTextLevel();
    }

    private void AnimateTextLevel()
    {
        nivel.gameObject.SetActive(true);
        nivel.transform.DOScale(Vector3.one * 2, 1f).OnComplete(() => 
        {
            nivel.gameObject.SetActive(false);
            nivel.transform.localScale = Vector3.one;
        });
    }

    private void FinishGame()
    {
        CancelInvoke();
        trapSpawner.SetVelocSpeedMultiplier(0);
        trapSpawner.spawnRate = int.MaxValue;
        velocityGame = 0f;
        player.ChangeVelocity(1f);
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        trapSpawner.SetVelocSpeedMultiplier(velocityGame);
        dotRotate.IncrementVelocity(velocityGame);
    }

}
