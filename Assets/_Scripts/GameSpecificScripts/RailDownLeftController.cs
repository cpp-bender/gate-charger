using FluffyUnderware.Curvy.Controllers;
using DG.Tweening;
using UnityEngine;

public class RailDownLeftController : MonoBehaviour
{
    private RailDownAnimations railDownAnimations;
    private StackManager stackManager;
    private ReferenceManager referenceManager;
    private CharacterMovement characterMovement;
    private Vector3 cloneBallPos;
    private Vector3 playerCloneBallPos;
    private Vector3 playerPosY = new Vector3(0, 0.3f, 0.2f);
    private GameObject cloneBall;
    private GameObject playerCloneBall;
    private GameObject player;
    private bool isJumpStarted = false;
    private float moveTweenTime = .1f;

    [Space(5)]
    public GameObject railDownInstantiatedLeftBall;
    public GameObject playerInstantiatedLeftBall;
    public GameObject tweenPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
        characterMovement = player.GetComponent<CharacterMovement>();
        railDownAnimations = player.GetComponent<RailDownAnimations>();
    }

    private void JumpOnBallAction()
    {
        if (stackManager.ballCount == 0)
        {
            characterMovement.canMoveForward = false;
            characterMovement.canMoveSideways = false;
            railDownAnimations.StartCoroutine(railDownAnimations.JackieOnBallAnimations());
            isJumpStarted = true;
            player.transform.parent = cloneBall.transform;
            cloneBallPos = cloneBall.transform.position;
            player.transform.position = cloneBallPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall))
        {
            GameObject touchedGO = other.gameObject;
            int ballIndexNumber = touchedGO.GetComponent<StackedBallController>().indexNumber;
            int ballCount = stackManager.ballCount;

            cloneBall = Instantiate(railDownInstantiatedLeftBall);
            cloneBall.SetActive(true);
            cloneBall.GetComponent<SplineController>().enabled = false;
            cloneBall.transform.position = touchedGO.transform.position;

            cloneBall.transform.DOMove(tweenPos.transform.position, moveTweenTime).Play()
                .OnStart(delegate
                {
                    cloneBall.GetComponent<Transform>().tag = "RailDownCloneBall";
                    cloneBall.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
                    cloneBall.GetComponent<RailDownBall>().materialType = other.GetComponent<StackedBallController>().materialType;
                    referenceManager.HandleStackMaterialQueue(ballIndexNumber);
                    stackManager.DoUnStack();
                    if (stackManager.ballCount != stackManager.stackedBallsMaterialsQueue.Count)
                    {
                        referenceManager.UpdateMaterialsQueue();
                    }
                })
                .OnComplete(delegate
                {
                    cloneBall.GetComponent<SplineController>().enabled = true;
                    JumpOnBallAction();
                });
        }
        if (other.CompareTag(Tags.Player))
        {
            if (stackManager.ballCount == 0)
            {
                playerCloneBall = Instantiate(playerInstantiatedLeftBall);
                playerCloneBall.SetActive(true);
                playerCloneBall.GetComponent<MeshRenderer>().enabled = false;
            
                characterMovement.canMoveForward = false;
                characterMovement.canMoveSideways = false;
                railDownAnimations.JackieStandingClap();
                player.transform.parent = playerCloneBall.transform;

                playerCloneBallPos = playerCloneBall.transform.position;
                player.transform.position = playerCloneBallPos - playerPosY;
            }
        }
    }
}
