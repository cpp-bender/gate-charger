using UnityEngine;

public class FakeTubeController : MonoBehaviour
{
    public float childrenPosX = 6;

    private void OnValidate()
    {
        SetPosX();
    }

    private void SetPosX()
    {
        var children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            child.transform.position = new Vector3(childrenPosX, child.transform.position.y, child.transform.position.z);
        }
    }
}
