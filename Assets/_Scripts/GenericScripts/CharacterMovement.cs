using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("DEBUG VALUES")]
    public bool canMoveForward;
    public bool canMoveSideways;
    public bool isGameAlreadyStarted;

    public float leftLimit = -2f;
    public float rightLimit = 2f;
    public float forwardSpeed = 4f;
    public float sideMovement;

    private float swipeSensivity;
    private float maximumSensivity = 25f;

    private Vector3 targetPos;

    private bool lockLeft;
    private bool lockRight;

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        TouchManager.instance.onTouchBegan += TouchBegan;
        TouchManager.instance.onTouchMoved += TouchMoved;
        TouchManager.instance.onTouchEnded += TouchEnded;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameAlreadyStarted)
        {
            isGameAlreadyStarted = true;
            player.StartGameForPlayer();
        }

        if (canMoveForward)
        {
            Movement();
        }
    }

    public void SetCharacterToCenter()
    {
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }

    private void TouchBegan(TouchInput touch)
    {
        targetPos = transform.position;
    }

    private void TouchEnded(TouchInput touch)
    {
        targetPos = transform.position;
        swipeSensivity = 0f;
        sideMovement = 0f;
    }

    private void TouchMoved(TouchInput touch)
    {
        //  Debug.Log(touch.DeltaScreenPosition.x);
        sideMovement = touch.DeltaScreenPosition.x;

        if (!canMoveSideways)
        {
            targetPos = new Vector3(0f, targetPos.y, targetPos.z);
            return;
        }

        swipeSensivity = Mathf.Abs(touch.DeltaScreenPosition.x);

        if (swipeSensivity > maximumSensivity)
        {
            swipeSensivity = maximumSensivity;
        }

        if (touch.DeltaScreenPosition.x > 0)
        {
            if (rightLimit < transform.position.x - (swipeSensivity / 1000f))
            {
                targetPos = new Vector3(transform.position.x + (swipeSensivity / 1000f), transform.position.y, transform.position.z);
            }
            else
            {
                targetPos = new Vector3(rightLimit, transform.position.y, transform.position.z);
            }

        }
        else if (touch.DeltaScreenPosition.x < 0)
        {
            if (leftLimit > transform.position.x + (swipeSensivity / 1000f))
            {
                targetPos = new Vector3(transform.position.x + (((swipeSensivity) / -1000f)), transform.position.y, transform.position.z);
            }
            else
            {
                targetPos = new Vector3(leftLimit, transform.position.y, transform.position.z);
            }

        }
        else
        {
            targetPos = transform.position;
        }
    }

    private void Movement()
    {
        if ((transform.position.x - targetPos.x < 0 && !lockRight) || (transform.position.x - targetPos.x > 0 && !lockLeft))
        {
            if (Time.timeScale >= 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * swipeSensivity / 2f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.unscaledDeltaTime * swipeSensivity / 2f);
            }
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Time.deltaTime * forwardSpeed);
    }
}
