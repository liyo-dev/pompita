using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI vidasText;
    private int currentHealth;
    private Animator animator;
    private Camera cam;
    public UnityEvent OnPlayerDeath;
    public GameObject VFXDead;
    public GameObject VFXReward;
    public GameObject VFXExtraLife;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }

    void Start()
    {
        currentHealth = 1;
        UpdateHealthText();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        AudioManager.Instance.PlayWih();
        animator.Play("Pompi vida");
        if (currentHealth > Utils.Variables.MaxHealth)
        {
            currentHealth = Utils.Variables.MaxHealth;
        }
        UpdateHealthText();
    }

    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        AudioManager.Instance.PlayAuch();
        if (currentHealth <= 0)
        {
            animator.Play("Pompi muerte total");
            Invoke(nameof(InstantiateDeaadVFX), 0.5f);
            Invoke(nameof(InstantiateDeaadVFX), 1f);
            currentHealth = 0;
            GameOver();
        }
        else
        {
            animator.Play("Pompi muerte");
            InstantiateDeaadVFX();
        }
        UpdateHealthText();
    }

    void InstantiateDeaadVFX()
    {
        ShakeCam();
        var dead_vfx = Instantiate(VFXDead, transform.position, Quaternion.identity);   
        Destroy(dead_vfx, 1f);
    }
    
    public void InstantiateRewardVFX()
    {
        var reward_vfx = Instantiate(VFXReward, transform.position, Quaternion.identity);   
        Destroy(reward_vfx, 1f);
    }
    
    public void InstantiateExtraLifeVFX()
    {
        var extraLife_vfx = Instantiate(VFXExtraLife, transform.position, Quaternion.identity);   
        Destroy(extraLife_vfx, 1f);
    }

    private void GameOver()
    {
        Invoke(nameof(RunGameOverEvent), 2f);
    }

    void RunGameOverEvent()
    {
        OnPlayerDeath.Invoke();
    }

    private void UpdateHealthText()
    {
        vidasText.text = "Vidas: " + currentHealth;
    }

    private void ShakeCam()
    {
        StartCoroutine(ShakeCameraCoroutine());
    }

    private IEnumerator ShakeCameraCoroutine()
    {
        Vector3 originalPosition = cam.transform.position;
        float duration = 0.5f;
        float magnitude = 0.8f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            cam.transform.position = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = originalPosition;
    }
}