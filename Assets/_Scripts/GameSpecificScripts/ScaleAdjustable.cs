using UnityEngine;

public class ScaleAdjustable : MonoBehaviour
{
    [Range(.1f, 5f)]
    public float scaleValue = 1;

    private void OnValidate()
    {
        ChangeScale();
    }

    private void ChangeScale()
    {
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
