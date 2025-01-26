using System.Collections;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Asigna el SpriteRenderer desde el Inspector
    public Sprite[] sprites; // Lista de imágenes a mostrar
    public float frameRate = 0.1f; // Tiempo entre frames

    private int currentFrame = 0;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Length > 0)
            StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[currentFrame];
            currentFrame = (currentFrame + 1) % sprites.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
