using FluffyUnderware.Curvy.Controllers;
using System.Collections.Generic;
using FluffyUnderware.Curvy;
using UnityEngine;
using System;

[SelectionBase]
public class LevelEndColliderController : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public FinishPlatformController finishPlatform;
    public GameObject finishRail;
    public GameObject railsBall;
    public CurvySpline levelEndSpline;
    public Action OnLastRailsBallStop;

    [Header("DEBUG VALUES. DO NOT EDIT THESE")]
    public List<float> splinePositions;
    public int totalPoint;
    public int totalBronzeBallCount;
    public int totalSilverBallCount;
    public int totalGoldBallCount;
    public int takenBallCount;
    public int ballMultiply = 1;

    private StackManager stackManager;
    private ReferenceManager referenceManager;
    private PlayerController player;
    private bool isLevelEndAnimPlaying;
    private int splinePosIndex;
    private Action OnLevelEndColliderHit;

    private void Awake()
    {
        OnLevelEndColliderHit = LevelEndColliderHit;
        OnLastRailsBallStop = LastRailsBallStop;
    }

    private void Start()
    {
        stackManager = StackManager.Instance;
        referenceManager = ReferenceManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        splinePositions = levelEndSpline.transform.GetComponent<LevelEndSplineController>().SetSplinePositions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.StackedBall) && !isLevelEndAnimPlaying)
        {
            var stackBall = other.gameObject.GetComponent<StackedBallController>();

            UpdateTotalBallCounts(stackBall);

            UpdateTotalPoint(stackBall.point);

            UpdateTakenBallCount();

            stackManager.DoUnStackForLevelEnd();

            CreateRailBall(other.gameObject);

            if (stackManager.ballCount == 0)
            {
                OnLevelEndColliderHit?.Invoke();
            }
        }

        if (other.CompareTag(Tags.Player) && !isLevelEndAnimPlaying)
        {
            OnLevelEndColliderHit?.Invoke();
        }
    }

    private void UpdateTotalBallCounts(StackedBallController stackBall)
    {
        if (stackBall.materialType == MaterialType.Bronze)
        {
            totalBronzeBallCount += stackBall.point;
        }
        else if (stackBall.materialType == MaterialType.Silver)
        {
            totalSilverBallCount += stackBall.point;
        }
        else if (stackBall.materialType == MaterialType.Gold)
        {
            totalGoldBallCount += stackBall.point;
        }
    }

    private void UpdateTotalPoint(int pointToAdd)
    {
        totalPoint += pointToAdd;
    }

    private void UpdateTakenBallCount()
    {
        takenBallCount++;
    }

    private void LevelEndColliderHit()
    {
        player.StopGameForPlayer();
        finishPlatform.canCheckRailsBalls = true;
        isLevelEndAnimPlaying = true;
    }

    private void LastRailsBallStop()
    {
        Camera.main.GetComponent<TestCameraFollow>().canFollowBalls = false;
        Camera.main.GetComponent<TestCameraFollow>().canFollowJackie = false;
        totalPoint *= takenBallCount;
        GameManager.instance.GateChargerLevelComplete(takenBallCount);
    }

    private void CreateRailBall(GameObject stackBall)
    {
        GameObject instantiatedBall = Instantiate(railsBall);
        finishPlatform.railsBalls.Add(instantiatedBall.GetComponent<RailsBallController>());
        instantiatedBall.GetComponent<RailsBallController>().ballPoint = stackBall.GetComponent<StackedBallController>().point;
        instantiatedBall.GetComponent<MeshRenderer>().material = stackBall.GetComponent<MeshRenderer>().material;
        instantiatedBall.GetComponent<SplineController>().Spline = levelEndSpline;
        instantiatedBall.GetComponent<RailsBallController>().splinePos = splinePositions[splinePosIndex];
        splinePosIndex++;
        ballMultiply++;
    }
}
