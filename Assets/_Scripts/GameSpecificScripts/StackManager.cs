using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class StackManager : SingletonMonoBehaviour<StackManager>
{
    [Header("DEBUG VALUES")]
    public bool mexicanWave;

    [Header("Scriptable Objects")]
    public MexicanWaveSettings mexicanWaveData;
    public PointSettings pointData;

    [Header("DO NOT EDIT THESE IN THE EDITOR")]
    public List<Material> stackedBallsMaterialsQueue;
    public List<StackedBallController> stackedBalls;
    [SerializeField, HideInInspector] MexicanWaveController mexicanWaveController;
    public bool turnOffBalls = true;
    public int ballCount = 0;
    public int maxBallCount = 15;

    private PlayerController player;
    private ReferenceManager referenceManager;
    private Action<GameObject> OnFirstStackTaken;
    private Action OnFirstStackReleased;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        referenceManager = ReferenceManager.Instance;
        OnFirstStackTaken = FirstStackTaken;
        OnFirstStackReleased = FirstStackReleased;
        stackedBallsMaterialsQueue = new List<Material>();
        //CacheAllStackedBalls();
        HandleStackedBallsActivity();
    }

    private void Update()
    {
        //UIManager.instance.ballCountText.GetComponent<TextMeshProUGUI>().text = "Ball Count: " + ballCount;

        mexicanWave = mexicanWaveController.CanDoMexicanWave;
    }

    private void CacheAllStackedBalls()
    {
        var stackedBallsGO = GameObject.FindGameObjectsWithTag(Tags.StackedBall);
        int currentStackBallNumber = 0;
        foreach (var stackedBallGO in stackedBallsGO)
        {
            var stackedBallController = stackedBallGO.GetComponent<StackedBallController>();
            stackedBalls.Add(stackedBallController);
            stackedBallController.indexNumber = currentStackBallNumber;
            currentStackBallNumber++;
        }
    }

    private void HandleStackedBallsActivity()
    {
        for (int i = 0; i < stackedBalls.Count; i++)
        {
            stackedBalls[i].gameObject.SetActive(!turnOffBalls);
        }
    }

    public void DoStack(GameObject ball)
    {
        var ballMat = ball.GetComponent<MeshRenderer>().material;
        if (ballCount == 0)
        {
            OnFirstStackTaken?.Invoke(ball);
            return;
        }

        if (ballCount < maxBallCount)
        {
            stackedBalls[ballCount].gameObject.SetActive(true);
            stackedBallsMaterialsQueue.Add(ballMat);
            ChangeMaterialTo(stackedBalls[ballCount], ballMat);
            stackedBalls[ballCount].SetMaterialState(ball.GetComponent<IBallController>().materialType);
            ballCount++;
            UpdateStackedBallsPoint();
            if (mexicanWaveController.CanDoMexicanWave)
            {
                StartCoroutine(DOMexicanWave());
            }
        }
    }

    public void UpdateStackedBallsPoint()
    {
        for (int i = 0; i < ballCount; i++)
        {
            if (stackedBalls[i].GetComponent<StackedBallController>().materialType == MaterialType.Bronze)
            {
                stackedBalls[i].point = pointData.BronzeBallPoint;
            }
            else if (stackedBalls[i].GetComponent<StackedBallController>().materialType == MaterialType.Silver)
            {
                stackedBalls[i].point = pointData.SilverBallPoint;
            }
            else if (stackedBalls[i].GetComponent<StackedBallController>().materialType == MaterialType.Gold)
            {
                stackedBalls[i].point = pointData.GoldBallPoint;
            }
        }
    }

    private IEnumerator DOMexicanWave()
    {
        mexicanWaveController.CanDoMexicanWave = false;
        if (mexicanWaveData.InverseAnimation)
        {
            for (int i = ballCount - 1; i >= 0; i--)
            {
                var ball = stackedBalls[i].gameObject;
                var tween = MexicanWave(ball);
                tween.Play();
                yield return tween.WaitForCompletion();
            }
        }
        else if (!mexicanWaveData.InverseAnimation)
        {
            for (int i = 0; i < ballCount; i++)
            {
                var ball = stackedBalls[i].gameObject;
                var tween = MexicanWave(ball);
                tween.Play();
                yield return tween.WaitForCompletion();
            }
        }
        mexicanWaveController.WaitForMexicanWaveToComplete(mexicanWaveData.NextTweenDelay);

        ResetStackedBallsScale(2.91f);
    }

    public void ResetStackedBallsScale(float scaleValue)
    {
        for (int i = 0; i < stackedBalls.Count; i++)
        {
            stackedBalls[i].transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
    }

    private void ChangeMaterialTo(StackedBallController stackedBall, Material mat)
    {
        var meshRenderer = stackedBall.GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }

    public void ClearAllStackedBallsMatQueue()
    {
        for (int i = 0; i < stackedBallsMaterialsQueue.Count; i++)
        {
            stackedBallsMaterialsQueue.RemoveAt(i);
        }
    }

    public void DoUnStack()
    {
        if (ballCount == 1)
        {
            OnFirstStackReleased?.Invoke();
            return;
        }

        if (ballCount > 0)
        {
            stackedBalls[ballCount - 1].gameObject.SetActive(false);
            ballCount--;
        }
    }

    public void DoUnStackForLevelEnd()
    {
        if (ballCount > 0)
        {
            stackedBalls[ballCount - 1].gameObject.SetActive(false);
            ballCount--;
        }
    }

    private Tween MexicanWave(GameObject ball)
    {
        return mexicanWaveController.GetMexicanWaveTween(ball, mexicanWaveData.TweenDamper, mexicanWaveData.StartTweenCompletionTime, mexicanWaveData.EndTweenCompletionTime);
    }

    public void DoUnStack(int index)
    {
        if (ballCount == 1)
        {
            OnFirstStackReleased?.Invoke();
            return;
        }

        if (ballCount > 0)
        {
            StartCoroutine(ShiftStackedBalls(index));
        }
    }

    public void DoUnStack(int index, int count, int limitCount)
    {
        if (limitCount < count - index)
        {
            for (int i = count - limitCount; i < count; i++)
            {
                stackedBalls[i].gameObject.SetActive(false);
                ballCount--;
                if (ballCount == 0)
                {
                    player.SetAnimationToRun();
                    break;
                }
            }

            for (int i = 0; i < count - limitCount; i++)
            {
                stackedBalls[i].gameObject.SetActive(true);
                ballCount++;
            }

            ballCount = 0;

            for (int i = 0; i < stackedBalls.Count; i++)
            {
                if (stackedBalls[i].gameObject.activeInHierarchy)
                {
                    ballCount++;
                }
            }
            return;
        }
        else
        {
            for (int i = index; i < count; i++)
            {
                stackedBalls[i].gameObject.SetActive(false);
                ballCount--;
                if (ballCount == 0)
                {
                    player.SetAnimationToRun();
                    break;
                }
            }
        }
    }

    private IEnumerator ShiftStackedBalls(int index)
    {
        stackedBalls[index].gameObject.SetActive(false);
        ballCount--;

        for (int i = index; i < ballCount; i++)
        {
            stackedBalls[i].GetComponent<SphereCollider>().enabled = false;
        }

        yield return new WaitForSeconds(.25f);

        for (int i = index; i < ballCount; i++)
        {
            if (!stackedBalls[i].gameObject.activeInHierarchy)
            {
                stackedBalls[i].gameObject.SetActive(true);
                stackedBalls[i].GetComponent<MeshRenderer>().material = stackedBalls[i + 1].GetComponent<MeshRenderer>().material;
                stackedBalls[i].GetComponent<StackedBallController>().materialType = stackedBalls[i + 1].GetComponent<StackedBallController>().materialType;
                stackedBalls[i].GetComponent<StackedBallController>().point = stackedBalls[i + 1].GetComponent<StackedBallController>().point;
                stackedBalls[i + 1].gameObject.SetActive(false);
            }
            yield return null;
        }

        for (int i = 0; i < stackedBalls.Count - 1; i++)
        {
            if (i < ballCount)
            {
                stackedBalls[i].gameObject.SetActive(true);
            }
            else
            {
                stackedBalls[i].gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(.25f);

        for (int i = 0; i < stackedBalls.Count - 1; i++)
        {
            stackedBalls[i].GetComponent<SphereCollider>().enabled = true;
        }

        referenceManager.UpdateMaterialsQueue();
    }

    private void FirstStackTaken(GameObject ball)
    {
        var ballMat = ball.GetComponent<MeshRenderer>().material;
        player.SetAnimationToPush();
        stackedBalls[ballCount].gameObject.SetActive(true);
        stackedBallsMaterialsQueue.Add(ballMat);
        ChangeMaterialTo(stackedBalls[ballCount], ballMat);
        stackedBalls[ballCount].SetMaterialState(ball.GetComponent<IBallController>().materialType);
        ballCount++;
        UpdateStackedBallsPoint();
        if (mexicanWaveController.CanDoMexicanWave)
        {
            StartCoroutine(DOMexicanWave());
        }
    }

    private void FirstStackReleased()
    {
        player.SetAnimationToRun();
        stackedBalls[0].gameObject.SetActive(false);
        ballCount--;
    }

    #region LEGACY CODE
    private void HandleStackBallsCollider(bool turnOff)
    {
        for (int i = 0; i < stackedBalls.Count; i++)
        {
            stackedBalls[i].GetComponent<SphereCollider>().enabled = turnOff;
        }
    }
    private void ShiftStackedBalls()
    {
        for (int i = 0; i < stackedBalls.Count; i++)
        {
            if (i < ballCount)
            {
                stackedBalls[i].gameObject.SetActive(true);
            }
            else
            {
                stackedBalls[i].gameObject.SetActive(false);
            }
        }
    }

    #endregion
}
