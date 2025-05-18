using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private int currentHealth;
    private Animator animator;
    private Camera cam;
    public UnityEvent OnDoTransition;
    public UnityAction OnPlayerDeathAction;
    public GameObject VFXDead;
    public GameObject VFXReward;
    public GameObject VFXExtraLife;
    public GameObject Life0;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public GameObject Life4;
    public GameObject Life5;
    public GameObject Life6;

    private void OnDestroy()
    {
        AudioManager.Instance.PlayMenu();
    }
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }

    void Start()
    {
        currentHealth = 1;
        ManageLifeUI();
    }

    public void AddPoint()
    {
        AudioManager.Instance.PlayWih();
        animator.Play("Pompi vida");
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;

        ManageLifeUI();

        AudioManager.Instance.PlayPop();

        if (currentHealth > Utils.Variables.MaxHealth)
        {
            currentHealth = Utils.Variables.MaxHealth;
        }
    }

    void ManageLifeUI()
    {
        Life0.SetActive(currentHealth == 0);
        Life1.SetActive(currentHealth == 1);
        Life2.SetActive(currentHealth == 2);
        Life3.SetActive(currentHealth == 3);
        Life4.SetActive(currentHealth == 4);
        Life5.SetActive(currentHealth == 5);
        Life6.SetActive(currentHealth == 6);

    }

    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        
        ManageLifeUI();
        
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
        AudioManager.Instance.PlayDeathMusic();
        OnPlayerDeathAction.Invoke();
        Invoke(nameof(RunTransition), 1f);
    }

    void RunTransition()
    {
        OnDoTransition.Invoke();
    }
    

    public void ChangeVelocity(float velocity)
    {
        animator.speed = velocity;
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