using UnityEngine;
using DG.Tweening;

public class FinishController : MonoBehaviour
{
    private const int stackedBallCount = 10;

    private StackManager stackManager;

    void Start()
    {
        stackManager = StackManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            player.GetComponent<Animator>().SetTrigger("FinishTurn");
            player.transform.DORotate(new Vector3(0f, 180f, 0f), 1.5f, RotateMode.Fast).Play();
            player.GetComponent<Animator>().SetTrigger("FinishDance");
            player.StopGameForPlayer();
        }
        else if (other.CompareTag("Stacked Ball"))
        {
            stackManager.DoUnStack();
        }
    }


}
