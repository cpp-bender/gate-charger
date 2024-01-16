using Random = UnityEngine.Random;
using System.Collections.Generic;
using DG.Tweening.Core.Enums;
using FluffyUnderware.Curvy;
using DG.Tweening;
using UnityEngine;

public class ReferenceManager : SingletonMonoBehaviour<ReferenceManager>
{
    [Header("OTHER DEPENDENCIES")]
    public BallThrowData ballData;
    public MexicanWaveSettings mexicanWaveData;
    public List<Material> ballMatsQueue;
    public float physicsBallPosY = .2f;

    private StackManager stackManager;

    //Pelin - Spline Curve List
    [Header("Spline Curve List"), Space(5f)]
    public List<CurvySpline> splineCurvePoints;

    public int ActiveSplineCurveIndex { get; set; }

    private CurvySpline activeSplineCurve;

    [Header("BallsInTube Controller List"), Space(5f)]
    public List<BallsInTubeController> ballsInTubeControllers;

    public int ActiveBallsInTubeControllerIndex { get; set; }

    private BallsInTubeController activeBallsInTubeController;

    protected override void Awake()
    {
        base.Awake();
        InitDOTween();
    }

    private void Start()
    {
        stackManager = StackManager.Instance;
        SetFirstSplineCurve();
        SetFirstBallsInTubeController();
    }

    #region Pelin Spline Curves
    public CurvySpline GetActiveSplinePoint()
    {
        return activeSplineCurve;
    }

    private void SetFirstSplineCurve()
    {
        activeSplineCurve = splineCurvePoints[ActiveSplineCurveIndex];
    }

    public void SetActiveSplineCurve(int activeSplineCurveIndex)
    {
        if (activeSplineCurveIndex < splineCurvePoints.Count)
        {
            activeSplineCurve = splineCurvePoints[activeSplineCurveIndex];
        }
    }
    #endregion

    #region BALLS IN TUBE CONTROLLERS
    public BallsInTubeController GetActiveBallsInTubeController()
    {
        return activeBallsInTubeController;
    }

    private void SetFirstBallsInTubeController()
    {
        activeBallsInTubeController = ballsInTubeControllers[ActiveBallsInTubeControllerIndex];
    }

    public void SetActiveBallsInTubeController(int index)
    {
        if (ActiveBallsInTubeControllerIndex < ballsInTubeControllers.Count)
        {
            activeBallsInTubeController = ballsInTubeControllers[index];
        }
    }

    #endregion

    private void InitDOTween()
    {
        //Default All DOTween Global Settings
        DOTween.Init(true, true, LogBehaviour.Default);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.maxSmoothUnscaledTime = .15f;
        DOTween.nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;
        DOTween.showUnityEditorReport = false;
        DOTween.timeScale = 1f;
        DOTween.useSafeMode = true;
        DOTween.useSmoothDeltaTime = false;
        DOTween.SetTweensCapacity(200, 50);

        //Default All DOTween Tween Settings
        DOTween.defaultAutoKill = true;
        DOTween.defaultEaseOvershootOrAmplitude = 1.70158f;
        DOTween.defaultEasePeriod = 0f;
        DOTween.defaultEaseType = Ease.Linear;
        DOTween.defaultLoopType = LoopType.Restart;
        DOTween.defaultRecyclable = false;
        DOTween.defaultTimeScaleIndependent = false;
        DOTween.defaultUpdateType = UpdateType.Normal;
    }

    public void ThrowBalls(Vector3 worldPos, int ballCount, int ballMatStartIndex, MaterialType matType)
    {
        var ballMatIndex = ballMatStartIndex;
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 spawnPos = worldPos + (Random.insideUnitSphere * ballData.RandomSpawnRadius) + ballData.SpawnOffset;
            GameObject ball = Instantiate(ballData.PhysicsBallPrefab, spawnPos, Quaternion.identity);

            ball.GetComponent<MeshRenderer>().material = stackManager.stackedBallsMaterialsQueue[ballMatIndex];
            ball.GetComponent<IBallController>().materialType = matType;
            ballMatIndex++;

            //ball.transform.position = new Vector3(ball.transform.position.x, physicsBallPosY, ball.transform.position.z);
            Vector3 randomDir = Random.insideUnitSphere.normalized;
            Vector3 forceDir = HandleDir(randomDir * ballData.ForceMultiplier);
            Vector3 torqueDir = HandleDir(randomDir * ballData.TorqueMultiplier);

            if (ballData.AddForce)
            {
                ball.GetComponent<Rigidbody>().AddRelativeForce(forceDir, ForceMode.Impulse);
            }

            if (ballData.AddTorque)
            {
                ball.GetComponent<Rigidbody>().AddRelativeTorque(torqueDir, ForceMode.Impulse);
            }

            if (ballData.WillDestroy)
            {
                Destroy(ball, ballData.LifeTime);
            }
        }

        ClearStackMaterialQueue(ballCount);
    }

    public void ThrowBalls(Vector3 worldPos, GameObject ball)
    {
        GameObject physicsBall = Instantiate(ballData.PhysicsBallPrefab, worldPos, Quaternion.identity);

        //physicsBall.transform.position = new Vector3(physicsBall.transform.position.x, physicsBallPosY, physicsBall.transform.position.z);

        physicsBall.GetComponent<MeshRenderer>().material = ball.GetComponent<MeshRenderer>().material;
        physicsBall.GetComponent<PhysicsBallController>().materialType = ball.GetComponent<RailDownBall>().materialType;

        Vector3 randomDir = Random.insideUnitSphere.normalized;

        #region ADDITIONAL CODE FOR FORCE AND TORQUE

        Vector3 forceDir = Vector3.forward * 10f;
        Vector3 torqueDir = HandleDir(randomDir * ballData.TorqueMultiplier);

        if (ballData.AddForce)
        {
            physicsBall.GetComponent<Rigidbody>().AddRelativeForce(forceDir, ForceMode.Impulse);
        }

        if (ballData.AddTorque)
        {
            physicsBall.GetComponent<Rigidbody>().AddRelativeTorque(torqueDir, ForceMode.Impulse);
        }

        if (ballData.WillDestroy)
        {
            Destroy(physicsBall, ballData.LifeTime);
        }
        #endregion
    }
    public void ClearStackMaterialQueue(int count)
    {
        for (int i = 0; i < count; i++)
        {
            stackManager.stackedBallsMaterialsQueue.RemoveAt(stackManager.stackedBallsMaterialsQueue.Count - 1);
        }
    }

    public void HandleStackMaterialQueue(int index)
    {
        for (int i = index; i < stackManager.stackedBallsMaterialsQueue.Count; i++)
        {
            stackManager.stackedBallsMaterialsQueue.RemoveAt(i);
        }
    }

    public void UpdateMaterialsQueue()
    {
        //TODO:Update material queue according to stacked balls
        ClearStackMaterialQueue(stackManager.stackedBallsMaterialsQueue.Count);
        for (int i = 0; i < stackManager.ballCount; i++)
        {
            stackManager.stackedBallsMaterialsQueue.Add(stackManager.stackedBalls[i].GetComponent<MeshRenderer>().material);
        }
    }

    private Vector3 HandleDir(Vector3 dir)
    {
        return new Vector3(dir.x, 0f, Mathf.Abs(dir.z));
    }
}
