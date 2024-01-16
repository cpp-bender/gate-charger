using System.Collections.Generic;
using FluffyUnderware.Curvy;
using UnityEngine;

public class LevelEndSplineController : MonoBehaviour
{
    [Header("SET THESE VALUES IN THE EDITOR")]
    public int splineThreshold = 1;
    public int splineStartIndex = 2;

    private CurvySpline curvySpline;

    private void Awake()
    {
        curvySpline = GetComponent<CurvySpline>();
    }

    public List<float> SetSplinePositions()
    {
        var splinePositions = new List<float>();
        for (int i = splineStartIndex; i < curvySpline.ControlPointCount; i += splineThreshold)
        {
            var currentControlPoint = transform.GetChild(i).GetComponent<CurvySplineSegment>();
            splinePositions.Add(currentControlPoint.Distance);
        }
        return splinePositions;
    }
}
