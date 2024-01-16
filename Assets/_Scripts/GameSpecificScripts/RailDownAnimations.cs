using System.Collections;
using UnityEngine;

public class RailDownAnimations : MonoBehaviour
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator JackieOnBallAnimations()
    {
        animator.SetTrigger(PlayerAnimationParams.jumpOnBallAnimTrigger);
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger(PlayerAnimationParams.sittingClapAnimTrigger);
    }

    public void JackieOffBallAnimations()
    {
        animator.SetTrigger(PlayerAnimationParams.runAnimTrigger);
    }

    public void JackieSitAnimation()
    {
        animator.SetTrigger(PlayerAnimationParams.sittingClapAnimTrigger);
    }

    public void JackieStandingClap()
    {
        animator.SetTrigger(PlayerAnimationParams.standingClapAnimTrigger);
    }
}
