using UnityEngine;

public class RailDownEndColliderController : MonoBehaviour
{
    private RailDownAnimations railDownAnimations;
    private CharacterMovement characterMovement;
    private GameObject player;
    private StackManager stackManager;
    private ReferenceManager referenceManager;
    public bool isPlayerParentNull;

    public float playerPosY;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        characterMovement = player.GetComponent<CharacterMovement>();
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
        railDownAnimations = player.GetComponent<RailDownAnimations>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            characterMovement.canMoveForward = true;
            characterMovement.canMoveSideways = true;
            player.transform.parent = null;

            if (player.transform.parent == null)
            {
                isPlayerParentNull = true;
            }

            player.transform.position = new Vector3(player.transform.position.x, playerPosY, player.transform.position.z);
            railDownAnimations.JackieOffBallAnimations();
        }
    }
}