using System.Collections;
using UnityEngine;
using DG.Tweening;

[SelectionBase]
public class GateController : MonoBehaviour
{
    [Header("DEBUG VALUES")]
    [SerializeField, HideInInspector] MexicanWaveController mexicanWaveController;

    private StackManager stackManager;
    private ReferenceManager referenceManager;
    private MexicanWaveSettings mexicanWaveData;

    private void Start()
    {
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
        mexicanWaveData = referenceManager.mexicanWaveData;
        mexicanWaveController.CanDoMexicanWave = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            other.GetComponent<StackedBallController>().UpdateMaterialType();
            stackManager.UpdateStackedBallsPoint();
            if (mexicanWaveController.CanDoMexicanWave)
            {
                GameObject ball = other.gameObject;
                StartCoroutine(DOMexicanWave());
            }
        }
    }

    private IEnumerator DOMexicanWave()
    {
        mexicanWaveController.CanDoMexicanWave = false;
        if (mexicanWaveData.InverseAnimation)
        {
            for (int i = stackManager.ballCount - 1; i >= 0; i--)
            {
                var ball = stackManager.stackedBalls[i].gameObject;
                var tween = MexicanWave(ball);
                tween.Play();
                yield return tween.WaitForCompletion();
            }
        }
        else if (!mexicanWaveData.InverseAnimation)
        {
            for (int i = 0; i < stackManager.ballCount; i++)
            {
                var ball = stackManager.stackedBalls[i].gameObject;
                var tween = MexicanWave(ball);
                tween.Play();
                yield return tween.WaitForCompletion();
            }
        }
        mexicanWaveController.WaitForMexicanWaveToComplete(mexicanWaveData.NextTweenDelay);

        stackManager.ResetStackedBallsScale(2.91f);
    }

    private Tween MexicanWave(GameObject ball)
    {
        return mexicanWaveController.GetMexicanWaveTween(ball, mexicanWaveData.TweenDamper, mexicanWaveData.StartTweenCompletionTime, mexicanWaveData.EndTweenCompletionTime);
    }
}
