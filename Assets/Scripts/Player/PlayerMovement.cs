using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float jumpForce = 10f;
    private bool canJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement.y = 0f;
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        animator.SetFloat("horiz", movement.x);

        if (Input.GetAxis("Horizontal") != 0f)
        {
            animator.SetFloat("lastHoriz", Input.GetAxis("Horizontal"));
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetFloat("lastHoriz") < -0.01f)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (movement.x <= -0.1f || movement.x >= 0.1f)
        {
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 7 is ground layer
        if (collision.gameObject.layer == 7)
        {
            // if this is the non wall collider
            if (collision.otherCollider.CompareTag("Player"))
            {
                canJump = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            // if this is the non wall collider
            if (collision.otherCollider.CompareTag("Player"))
            {
                canJump = false;
            }
        }
    }
}