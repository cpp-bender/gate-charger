using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomRigidbodyHandler : MonoBehaviour
{
    private enum UpdateType { Awake, Update }

    [SerializeField] CustomRigidbody customRigidbody;
    [SerializeField] UpdateType updateType = UpdateType.Awake;

    private Rigidbody body;
    private bool isUpdateTypeSetToAwake = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        if (updateType == UpdateType.Awake)
        {
            isUpdateTypeSetToAwake = true;
            customRigidbody.SetRigidbodyParams(body);
        }
    }

    private void Update()
    {
        if (!isUpdateTypeSetToAwake && updateType == UpdateType.Update)
        {
            customRigidbody.SetRigidbodyParams(body);
        }
    }
}
