using UnityEngine;

[SelectionBase]
public class StackedBallController : MonoBehaviour
{
    public MaterialType materialType = MaterialType.Bronze;

    [Header("DEBUG VALUES")]
    public float forwardRotation;
    public float sideRotation;
    public int indexNumber;
    public int point;

    private StackManager stackManager;
    private ReferenceManager referenceManager;
    private MeshRenderer meshRenderer;
    private CharacterMovement characterMovement;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        stackManager = FindObjectOfType<StackManager>();
        referenceManager = FindObjectOfType<ReferenceManager>();
        forwardRotation = 270f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.UnStackedBall))
        {
            var ball = other.gameObject;
            other.gameObject.SetActive(false);
            stackManager.DoStack(ball);
        }
        else if (other.CompareTag(Tags.PhysicsBall))
        {
            //Debug.LogError(gameObject.name);
            var ball = other.gameObject;
            other.gameObject.SetActive(false);
            stackManager.DoStack(ball);

        }
    }

    private void Update()
    {
        RotateTheBall();
    }

    public void RotateTheBall()
    {
        sideRotation = characterMovement.sideMovement * -20f;
        transform.Rotate(new Vector3(forwardRotation, 0f, sideRotation) * Time.deltaTime, Space.World);
    }

    public void TurnOffStackedBallGO()
    {
        gameObject.SetActive(false);
    }

    public void TurnOnStackedBallGO()
    {
        gameObject.SetActive(true);
    }

    public void UpdateMaterialType()
    {
        if (materialType == MaterialType.Bronze)
        {
            SetMaterialState(MaterialType.Silver);
            stackManager.stackedBallsMaterialsQueue[indexNumber] = referenceManager.ballMatsQueue[1];
            meshRenderer.material = referenceManager.ballMatsQueue[1];

        }
        else if (materialType == MaterialType.Silver)
        {
            SetMaterialState(MaterialType.Gold);
            stackManager.stackedBallsMaterialsQueue[indexNumber] = referenceManager.ballMatsQueue[2];
            meshRenderer.material = referenceManager.ballMatsQueue[2];
        }
    }

    public void SetMaterialState(MaterialType newMaterialType)
    {
        materialType = newMaterialType;
    }
}
