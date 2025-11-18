using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public float popScale = 1.3f;
    public float duration = 0.15f;
    Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale =transform.localScale;
        BeatDetector.Instance.OnBeat.AddListener(OnBeat);
    }

    // Update is called once per frame
    void OnBeat()
    {
        transform.DOKill();

        transform.DOScale(originalScale, duration).ChangeStartValue(originalScale*popScale).SetEase(Ease.InQuad);
    }
}
