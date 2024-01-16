using System.Collections.Generic;
using UnityEngine;

public class FinishPlatformController : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public LevelEndColliderController levelEndCollider;
    public List<RailsBallController> railsBalls;

    [Header("DEBUG VALUES")]
    public bool canCheckRailsBalls;
    public bool canAlreadyChecked;

    private void Awake()
    {
        railsBalls = new List<RailsBallController>();
    }

    private void Update()
    {
        if (canCheckRailsBalls && !canAlreadyChecked)
        {
            for (int i = 0; i < railsBalls.Count; i++)
            {
                if (!railsBalls[i].isAlreadyStopped)
                {
                    return;
                }
            }
            levelEndCollider.OnLastRailsBallStop?.Invoke();
            canAlreadyChecked = true;
        }
    }

}
