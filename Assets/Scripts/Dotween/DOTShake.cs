using DG.Tweening;
using UnityEngine;

public class DOTShake : MonoBehaviour
{
    void Start()
    {
        Sequence shakeSequence = DOTween.Sequence();
        shakeSequence.AppendCallback(() => ShakeOriginal())
                      .AppendInterval(0.3f)  // Intervalo entre animaciones
                      .AppendCallback(() => ScaleAndDescend());

        // Inicia la secuencia
        shakeSequence.Play();
    }

    private void ShakeOriginal()
    {
        // Animaci�n original
        transform.DOScale(.8f, 0.2f).SetEase(Ease.OutBack).Play();
    }

    private void ScaleAndDescend()
    {
        // Animaci�n de escala y descenso
        transform.DOScale(new Vector3(.5f, .5f, 1f), 0.2f).SetEase(Ease.OutQuad);
        transform.DOMoveY(transform.position.y - 20f, 0.2f).SetEase(Ease.OutQuad);
    }
}
