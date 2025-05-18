using DG.Tweening;
using UnityEngine;

public class DOTRotateSprite : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Eje de rotaci�n personalizado
    [SerializeField] private bool rotateClockwise = true; // Controla la direcci�n de rotaci�n

    void Start()
    {
        RotateSpriteOnItself();
    }

    void RotateSpriteOnItself()
    {
        int rotationDirection = rotateClockwise ? 1 : -1;

        transform.DORotate(rotationAxis * 360f * rotationDirection, rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear).Play();
    }
}
