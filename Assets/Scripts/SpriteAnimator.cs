using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public Image sourceImage; // Asigna el SpriteRenderer desde el Inspector
    public Sprite[] sprites; // Lista de imágenes a mostrar
    private float frameRate = 0.2f; // Tiempo entre frames

    private int currentFrame = 0;

    void Start()
    {
        if (sourceImage == null)
            return;
        sourceImage.sprite = sprites[currentFrame];

        if (sprites.Length > 0)
            StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            Debug.Log("Frame " + currentFrame);
            yield return new WaitForSeconds(frameRate);
            currentFrame = (currentFrame + 1) % sprites.Length;
            sourceImage.sprite = sprites[currentFrame];
        }
    }
}
