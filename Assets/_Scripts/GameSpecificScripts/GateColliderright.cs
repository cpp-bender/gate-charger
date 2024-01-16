using FluffyUnderware.Curvy.Controllers;
using UnityEngine.Serialization;
using UnityEngine;

public class GateColliderRight : MonoBehaviour
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

    //GateCollider Right Spline chosen
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            if (tubeController.totalBallCount <= 0)
            {
                return;
            }
          
            var cloneBall = Instantiate(instantiatedBallPrefab);
            cloneBall.SetActive(true);
            cloneBall.GetComponent<Transform>().tag = "Clone Ball";
            cloneBall.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            cloneBall.GetComponent<StackedBallController>().enabled = false;
            
            var cloneBallCurvySpline = cloneBall.GetComponent<SplineController>();
            cloneBallCurvySpline.Spline = referenceManager.splineCurvePoints[referenceManager.ActiveSplineCurveIndex];

            var touchedGO = other.gameObject;
            int ballIndexNumber = touchedGO.GetComponent<StackedBallController>().indexNumber;
            referenceManager.HandleStackMaterialQueue(ballIndexNumber);
            stackManager.DoUnStack(1);
            tubeController.UpdateCloneBallPosDamper();

            if (stackManager.ballCount != stackManager.stackedBallsMaterialsQueue.Count)
            {
                //Debug.Log("Materials updated");
                referenceManager.UpdateMaterialsQueue();
            }
        }
    }
}
