using UnityEngine;

public class CartScript : MonoBehaviour
{
    [SerializeField] private Animator[] tireAnimators;  // Array of Animators
    public int speed;
    public Rigidbody cart;

    public GameManager gameManager;

    void Start()
    {
        speed = 5;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.GamePaused();
        }
    }

    void Movement()
    {
        float horizMove = Input.GetAxis("Horizontal");
        cart.linearVelocity = new Vector3(horizMove * speed, cart.linearVelocity.y,cart.linearVelocity.z);

        if (horizMove > 0)
        {
            SetTireAnimations(true, false);
        }
        else if (horizMove < 0)
        {
            SetTireAnimations(false, true);
        }
        else
        {
            SetTireAnimations(false, false);
        }
    }

    // Helper method to update all tire animators
    private void SetTireAnimations(bool isFrontRotate, bool isBackRotate)
    {
        foreach (Animator animator in tireAnimators)
        {
            animator.SetBool("isFrontRotate", isFrontRotate);
            animator.SetBool("isBackRotate", isBackRotate);
        }
    }
}
