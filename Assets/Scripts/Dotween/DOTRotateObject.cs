using UnityEngine;
using DG.Tweening;

public class DOTRotateObject : MonoBehaviour
{
    public float rotationAmount = 360f;
    public float duration = 2f;
    public LoopType loopType = LoopType.Restart; 
    public Ease easeType = Ease.Linear;

    private Tween rotationTween;

    void Start()
    {
        rotationTween = transform.DORotate(new Vector3(0, 0, -rotationAmount), duration, RotateMode.FastBeyond360)
            .SetEase(easeType)
            .SetLoops(-1, loopType).Play();
    }

    public void IncrementVelocity(float velocity)
    {
        rotationTween.timeScale = velocity;
    }
}
