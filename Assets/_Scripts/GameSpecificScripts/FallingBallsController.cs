using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBallsController : MonoBehaviour
{
    private StackManager stackManager;
    private ReferenceManager referenceManager;

    public OpenGateController openGateController;

    private void Start()
    {
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            if (openGateController.isActiveBridge == false)
            {
                var touchedBallGO = other.GetComponent<StackedBallController>();
                MaterialType ballMatType = touchedBallGO.GetComponent<StackedBallController>().materialType;
                int ballCount = stackManager.ballCount;
                int ballIndexNumber = touchedBallGO.indexNumber;
                for (int i = 0; i < ballCount - ballIndexNumber; i++)
                {
                    stackManager.DoUnStack();
                }
                referenceManager.ThrowBalls(other.gameObject.transform.position, ballCount - ballIndexNumber, ballIndexNumber, ballMatType);

            }
        }
    }
}
