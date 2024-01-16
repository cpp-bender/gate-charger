using FluffyUnderware.Curvy.Controllers;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

public class BallsInTubeController : MonoBehaviour
{
    public GameObject instantiatedBallPrefab;
    public TubeMovementController tubeController;
    public OpenGateController openGateController;

    private StackManager stackManager;
    private ReferenceManager referenceManager;

    
    private void Start()
    {
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
    }

    //Create instantiated ball and add the current Spline Curvy inside.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            if (tubeController.totalBallCount <= 0)
            {
                return;
            }

            GameObject touchedGO = other.gameObject;
            int ballIndexNumber = touchedGO.GetComponent<StackedBallController>().indexNumber;
            int ballCount = stackManager.ballCount;

            var cloneBall = Instantiate(instantiatedBallPrefab);
            cloneBall.SetActive(true);
            cloneBall.GetComponent<Transform>().tag = "Clone Ball";
            cloneBall.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            cloneBall.GetComponent<StackedBallController>().enabled = false;
            var cloneBallCurvySpline = cloneBall.GetComponent<SplineController>();
            cloneBallCurvySpline.Spline = referenceManager.splineCurvePoints[referenceManager.ActiveSplineCurveIndex];
            
            stackManager.DoUnStack(ballIndexNumber);

            tubeController.UpdateCloneBallPosDamper();

            openGateController.AddCloneBall(cloneBall);

            #region LEGACY CODE
            /*
             *             Debug.LogError(other.GetComponent<StackedBallController>().materialType);
             *             
            if (ballCount - ballIndexNumber == 1)
            {
                //Takes only one
                var cloneBall = Instantiate(instantiatedBallPrefab);
                cloneBall.SetActive(true);
                cloneBall.GetComponent<Transform>().tag = "Clone Ball";
                cloneBall.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
                cloneBall.GetComponent<StackedBallController>().enabled = false;
                var cloneBallCurvySpline = cloneBall.GetComponent<SplineController>();
                cloneBallCurvySpline.Spline = referenceManager.splineCurvePoints[referenceManager.ActiveSplineCurveIndex];
                referenceManager.HandleStackMaterialQueue(ballIndexNumber);
                stackManager.DoUnStack();
                tubeController.UpdateCloneBallPosDamper();
                if (stackManager.ballCount != stackManager.stackedBallsMaterialsQueue.Count)
                {
                    referenceManager.UpdateMaterialsQueue();
                }

                openGateController.AddCloneBall(cloneBall);
            }
            else
            {
                //Takes more than one

                var mats = new List<Material>();

                stackManager.DoUnStack(ballIndexNumber, ballCount, tubeController.totalBallCount);

                int x = 0;

                if (tubeController.totalBallCount < ballCount - ballIndexNumber)
                {
                    x = ballCount - tubeController.totalBallCount;
                }
                else
                {
                    x = ballIndexNumber;
                }

                for (int i = x; i < ballCount; i++)
                {
                    mats.Add(stackManager.stackedBalls[i].GetComponent<MeshRenderer>().material);
                }

                int startMatIndex = mats.Count - 1;

                for (int i = 0; i < mats.Count; i++)
                {
                    var cloneBall = Instantiate(instantiatedBallPrefab);
                    cloneBall.SetActive(true);
                    cloneBall.GetComponent<Transform>().tag = "Clone Ball";
                    cloneBall.GetComponent<MeshRenderer>().material = mats[startMatIndex];
                    startMatIndex--;
                    cloneBall.GetComponent<StackedBallController>().enabled = false;
                    var cloneBallCurvySpline = cloneBall.GetComponent<SplineController>();
                    cloneBallCurvySpline.Spline = referenceManager.splineCurvePoints[referenceManager.ActiveSplineCurveIndex];
                    tubeController.UpdateCloneBallPosDamper();
                    openGateController.AddCloneBall(cloneBall);
                }

                referenceManager.HandleStackMaterialQueue(ballIndexNumber);
                if (stackManager.ballCount != stackManager.stackedBallsMaterialsQueue.Count)
                {
                    referenceManager.UpdateMaterialsQueue();
                }
            }
            */
            #endregion
        }
    }
}
