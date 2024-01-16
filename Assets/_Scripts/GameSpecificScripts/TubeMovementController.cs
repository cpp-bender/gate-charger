using UnityEngine.Serialization;
using UnityEngine;
using System;

[Serializable]
public class TubeMovementController
{
    [Header("DEBUG VALUES")]
    public int totalBallCount = 4;
    public float splinePos = 0f;
    public float splinePosDamper = .3f;
    public float startDelay;
    public float delayDamper = 0f;

    public void UpdateCloneBallPosDamper()
    {
        totalBallCount--;
        splinePos -= splinePosDamper;
        startDelay += delayDamper;
    }
}
