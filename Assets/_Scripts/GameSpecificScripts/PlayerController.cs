using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    private StackManager stackManager;
    private CharacterMovement characterMovement;
    private Animator animator;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stackManager = GameObject.Find("Stack Manager").GetComponent<StackManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.UnStackedBall) && stackManager.ballCount == 0)
        {
            var ball = other.gameObject;
            other.gameObject.SetActive(false);
            stackManager.DoStack(ball);
        }
        else if (other.CompareTag(Tags.PhysicsBall) && stackManager.ballCount == 0)
        {
            var ball = other.gameObject;
            other.gameObject.SetActive(false);
            stackManager.DoStack(ball);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PhysicsBall) && stackManager.ballCount == 0)
        {
            var ballMat = collision.gameObject;
            collision.gameObject.SetActive(false);
            stackManager.DoStack(ballMat);
        }
    }

    public void StartGameForPlayer()
    {
        characterMovement.canMoveForward = true;
        characterMovement.canMoveSideways = true;
        animator.SetTrigger(PlayerAnimationParams.runAnimTrigger);
    }

    public void StopGameForPlayer()
    {
        characterMovement.canMoveForward = false;
        characterMovement.canMoveSideways = false;
        animator.SetTrigger(PlayerAnimationParams.idleAnimTrigger);
    }

    public void SetAnimationToRun()
    {
        animator.SetTrigger(PlayerAnimationParams.runAnimTrigger);
    }

    public void SetAnimationToPush()
    {
        animator.SetTrigger(PlayerAnimationParams.pushAnimTrigger);
    }
}
