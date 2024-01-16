using UnityEngine;

[SelectionBase]
public class PhysicsBallController : MonoBehaviour, IBallController
{
    public MaterialType materialType = MaterialType.Bronze;

    MaterialType IBallController.materialType { get => materialType; set => materialType = value; }
}
