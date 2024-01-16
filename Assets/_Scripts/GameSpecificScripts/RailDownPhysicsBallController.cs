using Random = UnityEngine.Random;
using UnityEngine;

public class RailDownPhysicsBallController : MonoBehaviour
{
    private StackManager stackManager;
    private ReferenceManager referenceManager;
    private float spawnOffset = 0f;
    private GameObject player;

    public float spawnPosY;
    public RailDownEndColliderController railDownEndColliderController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.RailDownCloneBall))
        {
            var spawnPos = gameObject.transform.position + new Vector3(0f, spawnPosY, spawnOffset);
            spawnOffset += .1f;
            referenceManager.ThrowBalls(spawnPos, other.gameObject);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;


            if (railDownEndColliderController.isPlayerParentNull)
            {
                other.gameObject.SetActive(false);
            }            
        }
    }
}
