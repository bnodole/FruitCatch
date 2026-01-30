using UnityEngine;

public class CartScript : MonoBehaviour
{
    [SerializeField] private Animator[] tireAnimators;

    public float speed = 8f;
    public float moveSmoothness = 20f;
    public Rigidbody cart;

    public float leftLimit = -6f;
    public float rightLimit = 3.5f;

    private float targetX;
    private float previousX;

    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetX = cart.position.x;
        previousX = cart.position.x;
    }

    void Update()
    {
        HandleKeyboard();
        HandleTouch();

        if (Input.GetKeyDown(KeyCode.Escape))
            gameManager.GamePaused();
    }

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(targetX, cart.position.y, cart.position.z);

        cart.MovePosition(Vector3.Lerp(
            cart.position,
            targetPos,
            moveSmoothness * Time.fixedDeltaTime
        ));

        HandleTireAnimation();
    }

    // -------- KEYBOARD --------
    void HandleKeyboard()
    {
        float horiz = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horiz) > 0.01f)
        {
            targetX += horiz * speed * Time.deltaTime;
            targetX = Mathf.Clamp(targetX, leftLimit, rightLimit);
        }
    }

    // -------- TOUCH --------
    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        Plane plane = new Plane(Vector3.forward, cart.position);
        float dist;

        if (plane.Raycast(ray, out dist))
        {
            Vector3 worldPoint = ray.GetPoint(dist);
            targetX = Mathf.Clamp(worldPoint.x, leftLimit, rightLimit);
        }
    }

    // -------- TIRE ANIMATION --------
    void HandleTireAnimation()
    {
        float deltaX = cart.position.x - previousX;

        if (Mathf.Abs(deltaX) > 0.001f)
        {
            bool right = deltaX > 0;
            SetTireAnimations(right, !right);
        }
        else
        {
            SetTireAnimations(false, false);
        }

        previousX = cart.position.x;
    }

    private void SetTireAnimations(bool isFrontRotate, bool isBackRotate)
    {
        foreach (Animator animator in tireAnimators)
        {
            animator.SetBool("isFrontRotate", isFrontRotate);
            animator.SetBool("isBackRotate", isBackRotate);
        }
    }
}
