using UnityEngine;

[CreateAssetMenu(menuName = "Gate Charger / Ball Throw Data", fileName = "Ball Throw Data")]
public class BallThrowData : ScriptableObject
{
    [Header("Game Object")]
    [SerializeField, Space(5f)] GameObject physicsBallPrefab;

    [Header("Force Params")]
    [SerializeField] bool addForce = true;
    [SerializeField] float forceMultiplier = 50f;

    [Header("Torque Params"), Space(5f)]
    [SerializeField] bool addTorque = true;
    [SerializeField] float torqueMultiplier = 50f;

    [Header("Other Params"), Space(5f)]
    [SerializeField] Vector3 spawnOffset = Vector3.zero;
    [SerializeField] float randomSpawnRadius = 1f;
    [SerializeField] bool willDestroy = false;
    [SerializeField] float lifeTime = 3f;

    public GameObject PhysicsBallPrefab { get => physicsBallPrefab; set => physicsBallPrefab = value; }
    public bool AddForce { get => addForce; set => addForce = value; }
    public bool AddTorque { get => addTorque; set => addTorque = value; }
    public float LifeTime { get => lifeTime; set => lifeTime = value; }
    public float ForceMultiplier { get => forceMultiplier; set => forceMultiplier = value; }
    public float TorqueMultiplier { get => torqueMultiplier; set => torqueMultiplier = value; }
    public float RandomSpawnRadius { get => randomSpawnRadius; set => randomSpawnRadius = value; }
    public Vector3 SpawnOffset { get => spawnOffset; set => spawnOffset = value; }
    public bool WillDestroy { get => willDestroy; set => willDestroy = value; }
}
