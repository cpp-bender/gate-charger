using System.Collections;
using UnityEngine;

public class FallAnimationController : MonoBehaviour
{
    private StackManager stackManager;
    private Animator playerAnim;
    private Rigidbody playerBody;
    private GameObject player;
    private GameObject mainCamera;

    public OpenGateController openGateController;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera);
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        stackManager = StackManager.Instance;
        playerBody = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (openGateController.isActiveBridge == false)
            {
                StartCoroutine(BridgeFall());
                mainCamera.GetComponent<TestCameraFollow>().enabled = false;
                GameManager.instance.LevelFail();
            }
        }
    }

    private IEnumerator BridgeFall()
    {
        player.GetComponent<Animator>().SetTrigger(PlayerAnimationParams.tripTrigger);
        yield return new WaitForSeconds(0.2f);
        playerBody.useGravity = true;
        playerBody.constraints = RigidbodyConstraints.FreezePositionX;
        playerBody.constraints = RigidbodyConstraints.FreezePositionZ;
        playerBody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
