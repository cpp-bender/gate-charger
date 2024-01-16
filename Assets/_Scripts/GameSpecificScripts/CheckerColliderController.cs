using UnityEngine;
using DG.Tweening;
using System;

public class CheckerColliderController : MonoBehaviour
{
    [Header("DEPENDENCIES"), Space(5f)]
    public GameObject levelEndCamObj;

    [Header("CAM TWEEN PARAMS")]
    public float camTweenCompletionTime;
    public float camTweenDelay;
    public Ease camTweenEase;

    private CharacterMovement player;
    private TestCameraFollow cam;
    private bool isPlayerAlreadyConstant = false;
    private Action CamPosChangedForLevelEnd;
    private Tween camMoveTween;
    private Tween camRotateTween;

    private void Awake()
    {
        cam = Camera.main.GetComponent<TestCameraFollow>();
        CamPosChangedForLevelEnd = OnCamPosChangedForLevelEnd;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<CharacterMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerAlreadyConstant && other.CompareTag(Tags.Player))
        {
            MakePlayerConstant();
            CamPosChangedForLevelEnd?.Invoke();

        }
        else if (!isPlayerAlreadyConstant && other.CompareTag(Tags.StackedBall))
        {
            MakePlayerConstant();
            CamPosChangedForLevelEnd?.Invoke();
        }
    }

    private void MakePlayerConstant()
    {
        isPlayerAlreadyConstant = true;
        player.canMoveSideways = false;
        player.SetCharacterToCenter();
        player.leftLimit = 0f;
        player.rightLimit = 0f;
    }

    private void OnCamPosChangedForLevelEnd()
    {
        camMoveTween = cam.transform.DOMove(levelEndCamObj.transform.position, camTweenCompletionTime).SetDelay(camTweenDelay).SetEase(camTweenEase)
             .OnStart(delegate
             {
                 cam.canFollowJackie = false;
             })
             .OnComplete(delegate
             {

             });

        camRotateTween = cam.transform.DOLocalRotate(new Vector3(45f, 0f, 0f), camTweenCompletionTime, RotateMode.Fast).SetDelay(camTweenDelay).SetEase(camTweenEase)
            .OnStart(delegate
            {
                cam.canFollowBalls = true;
            })
            .OnComplete(delegate
            {

            });

        camMoveTween.Play();
        camRotateTween.Play();
    }
}
