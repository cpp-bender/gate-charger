using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public class MexicanWaveController
{
    private Tween mexicanWaveTween;
    private bool canDoMexicanWave = true;

    public bool CanDoMexicanWave { get => canDoMexicanWave; set => canDoMexicanWave = value; }

    public void WaitForMexicanWaveToComplete(float completionTime)
    {
        float counter = 0;
        DOTween.To(() => counter, x => counter = x, 1f, completionTime)
            .OnComplete(delegate
            {
                canDoMexicanWave = true;
            })
            .Play();
    }

    public Tween GetMexicanWaveTween(GameObject ball, float mexicanWaveTweenDamper, float mexicanWaveStartTweenTime, float mexicanWaveEndTweenTime)
    {
        float ballNextScale = ball.transform.localScale.x + mexicanWaveTweenDamper;
        mexicanWaveTween = ball.transform.DOScale(ballNextScale, mexicanWaveStartTweenTime)
            .OnComplete(delegate
            {
                ballNextScale = ball.transform.localScale.x - mexicanWaveTweenDamper;
                ball.transform.DOScale(ballNextScale, mexicanWaveEndTweenTime).Play();
                StackManager.Instance.stackedBalls[0].transform.localScale = new Vector3(ballNextScale, ballNextScale, ballNextScale);
            });
        return mexicanWaveTween;
    }
}
