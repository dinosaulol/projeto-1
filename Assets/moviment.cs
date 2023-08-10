using UnityEngine;

public class moviment : MonoBehaviour
{
    void start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform pé;
    [SerializeField] private LayerMask chaoLayer;
    Animator anim;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0f);
        }

        Flip();
    }
    private void Move()
    {
        anim.SetBool("Walking", rb.velocity.x != 0 && IsGrounded());
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(pé.position, 0.2f, chaoLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}