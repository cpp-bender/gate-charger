using UnityEngine;

public class TestCameraFollow : MonoBehaviour
{
    [Header("DEBUG VALUES")]
    public bool canFollowJackie;
    public bool canFollowBalls;
    public GameObject target;
    public Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(Tags.Player);
        offset = target.transform.position - transform.position;
        canFollowJackie = true;
    }

    void Update()
    {
        if (canFollowJackie)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position - offset, Time.deltaTime * 10f);
        }
        else if (canFollowBalls)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward, Time.deltaTime * 1.5f);
        }
    }
}
