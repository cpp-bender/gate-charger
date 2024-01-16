using UnityEngine;

[CreateAssetMenu(menuName = "Gate Charger / Rigidbody Handler", fileName = "Rigidbody")]
public class CustomRigidbody : ScriptableObject
{
    [SerializeField, Space(5)] float mass = 1f;
    [SerializeField, Space(5)] float drag = 0f;
    [SerializeField, Space(5)] float angularDrag = 0.05f;
    [SerializeField, Space(5)] RigidbodyInterpolation interpolation = RigidbodyInterpolation.None;
    [SerializeField, Space(5)] CollisionDetectionMode collisionDetectionMode = CollisionDetectionMode.Discrete;
    [SerializeField, Space(5)] bool useGravity = false;
    [SerializeField, Space(5)] bool isKinematic = false;
    [SerializeField, Space(5)] RigidbodyConstraints constraint1 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint2 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint3 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint4 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint5 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint6 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint7 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint8 = RigidbodyConstraints.None;
    [SerializeField, Space(5)] RigidbodyConstraints constraint9 = RigidbodyConstraints.None;

    public void SetRigidbodyParams(Rigidbody body)
    {
        body.mass = mass;
        body.drag = drag;
        body.angularDrag = angularDrag;
        body.interpolation = interpolation;
        body.collisionDetectionMode = collisionDetectionMode;
        body.isKinematic = isKinematic;
        body.useGravity = useGravity;
        body.constraints = constraint1 | constraint2 | constraint3 | constraint4 | constraint5 | constraint6 | constraint7 | constraint8 | constraint9;
    }

    public float Mass { get => mass; }
    public float Drag { get => drag; }
    public float AngularDrag { get => angularDrag; }
    public RigidbodyInterpolation Interpolation { get => interpolation; }
    public CollisionDetectionMode CollisionDetectionMode { get => collisionDetectionMode; }
    public RigidbodyConstraints Constraint1 { get => constraint1; }
    public RigidbodyConstraints Constraint2 { get => constraint2; }
    public RigidbodyConstraints Constraint3 { get => constraint3; }
    public RigidbodyConstraints Constraint4 { get => constraint4; }
    public RigidbodyConstraints Constraint5 { get => constraint5; }
    public RigidbodyConstraints Constraint6 { get => constraint6; }
    public RigidbodyConstraints Constraint7 { get => constraint7; }
    public RigidbodyConstraints Constraint8 { get => constraint8; }
    public RigidbodyConstraints Constraint9 { get => constraint9; }
    public bool UseGravity { get => useGravity; }
    public bool IsKinematic { get => isKinematic; }
}
