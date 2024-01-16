using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using DG.Tweening;

public class RailsBallController : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public GameObject gemUIPrefab;

    [Header("DEBUG VALUES")]
    public float splinePos;
    public bool isAlreadyStopped;
    public int ballPoint;

    private SplineController splineController;

    private void Awake()
    {
        splineController = GetComponent<SplineController>();
    }

    private void Update()
    {
        if (!isAlreadyStopped && splineController.Position >= splinePos)
        {
            splineController.Pause();
            isAlreadyStopped = true;
            Vector3 gemStartPos = Camera.main.WorldToScreenPoint(transform.position);
            GameObject gem = Instantiate(gemUIPrefab, gemStartPos, Quaternion.identity, UIManager.instance.gameObject.transform);
            Vector3 gemEndPos = UIManager.instance.gemIcons[0].transform.position;
            gem.transform.DOMove(gemEndPos, .3f)
                .OnStart(delegate
                {
                    GameManager.instance.CollectGem(ballPoint);
                })
                .OnComplete(delegate
                {
                    gem.gameObject.SetActive(false);
                })
                .Play();
        }
    }
}
