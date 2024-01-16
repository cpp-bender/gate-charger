using UnityEngine;

[SelectionBase]
public class ObstacleController : MonoBehaviour
{
    private StackManager stackManager;
    private ReferenceManager referenceManager;

    private void Start()
    {
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            var touchedBallGO = other.GetComponent<StackedBallController>();
            MaterialType ballMatType = touchedBallGO.GetComponent<StackedBallController>().materialType;
            int ballCount = stackManager.ballCount;
            int ballIndexNumber = touchedBallGO.indexNumber;
            for (int i = 0; i < ballCount - ballIndexNumber; i++)
            {
                stackManager.DoUnStack();
            }
            referenceManager.ThrowBalls(new Vector3(transform.position.x, transform.position.y + 0.09f, transform.position.z), ballCount - ballIndexNumber, ballIndexNumber, ballMatType);
        }
    }
}
