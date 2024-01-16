using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using DG.Tweening;
using System;

public class RailDownBall : MonoBehaviour, IBallController
{
    public MaterialType materialType = MaterialType.Bronze;
    public bool shouldStop = false;

    private bool isAlreadyStopped = false;
    private SplineController splineController;
    private Action SplinePathComplete;

    MaterialType IBallController.materialType { get => materialType; set => materialType = value; }

    private void Awake()
    {
        splineController = GetComponent<SplineController>();
        SplinePathComplete = OnSplinePathComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldStop && !isAlreadyStopped && splineController.Position >= splineController.Spline.ControlPointsList[8].Distance)
        {
            splineController.Pause();
            isAlreadyStopped = true;
            splineController.enabled = false;
            SplinePathComplete?.Invoke();
        }
    }

    private void OnSplinePathComplete()
    {
        var currentPosY = transform.position.y;
        transform.DOMoveY(currentPosY - .5f, .1f).Play();
    }
}
