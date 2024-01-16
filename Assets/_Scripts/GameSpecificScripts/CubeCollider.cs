using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    private SplineController splineController;
    private float distance;

    public float timer = 0f;

    private void Awake()
    {
        splineController = GetComponent<SplineController>();
        distance = splineController.Length;
    }
}
