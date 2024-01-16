using UnityEngine;

public class Counter : MonoBehaviour
{
    private ReferenceManager referenceManager;
    private PlayerController player;

    private void Start()
    {
        referenceManager = ReferenceManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //Increment to the next Curly Spline & Gate Count 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            referenceManager.SetActiveSplineCurve(++referenceManager.ActiveSplineCurveIndex);
            referenceManager.SetActiveBallsInTubeController(++referenceManager.ActiveBallsInTubeControllerIndex);
        }
    }
}
