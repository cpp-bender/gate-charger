using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using DG.Tweening;
using System;

public class InstantiatedBallController : MonoBehaviour
{
    private SplineController splineController;
    private ReferenceManager referenceManager;
    private float splinePos;
    private float startDelay;
    private Action OnRouteComplete;
    private bool isAlreadyStopped;

    private void Awake()
    {
        splineController = GetComponent<SplineController>();
        OnRouteComplete = RouteComplete;
        referenceManager = ReferenceManager.Instance;
        splinePos = referenceManager.GetActiveBallsInTubeController().tubeController.splinePos;
        startDelay = referenceManager.GetActiveBallsInTubeController().tubeController.startDelay;
    }

    private void OnEnable()
    {
        float timer = 0f;
        DOTween.To(() => timer, x => x = timer, 1, startDelay)
            .OnStart(delegate
            {
                splineController.Pause();
            })
            .OnComplete(delegate
            {
                splineController.Play();
            })
            .Play();
    }

    private void Update()
    {
        if (!isAlreadyStopped && splineController.Position >= splinePos)
        {
            OnRouteComplete.Invoke();
        }
    }

    private void RouteComplete()
    {
        splineController.Pause();
        isAlreadyStopped = true;
    }
}
