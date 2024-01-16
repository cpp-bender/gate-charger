using FluffyUnderware.Curvy.Controllers;
using UnityEngine.Serialization;
using UnityEngine;

public class GateColliderLeft : MonoBehaviour
{
    public GameObject instantiatedBallPrefab;
    [FormerlySerializedAs("Values")] public TubeMovementController tubeController;

    private StackManager stackManager;
    private ReferenceManager referenceManager;

    private void Start()
    {
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
    }

    //GateCollider Left Spline chosen
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            if (tubeController.totalBallCount <= 0)
            {
                return;
            }
            var touchedGO = other.gameObject;
            var cloneBall = Instantiate(instantiatedBallPrefab);
            cloneBall.SetActive(true);
            cloneBall.GetComponent<Transform>().tag = "Clone Ball";
            cloneBall.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            cloneBall.GetComponent<StackedBallController>().enabled = false;
            var cloneBallCurvySpline = cloneBall.GetComponent<SplineController>();
            cloneBallCurvySpline.Spline = referenceManager.splineCurvePoints[referenceManager.ActiveSplineCurveIndex];
            int ballIndexNumber = touchedGO.GetComponent<StackedBallController>().indexNumber;
            referenceManager.HandleStackMaterialQueue(ballIndexNumber);
            //stackManager.DoUnStack(ballIndexNumber);
            tubeController.UpdateCloneBallPosDamper();
            if (stackManager.ballCount != stackManager.stackedBallsMaterialsQueue.Count)
            {
                //Debug.Log("Materials updated");
                referenceManager.UpdateMaterialsQueue();
            }
        }
    }
}
