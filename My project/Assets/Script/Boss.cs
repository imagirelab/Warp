using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D playerRb;
    public float predictionTime = 0.5f; // 0.5•bŒã‚ğ—\‘ª
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;

    private Rigidbody2D rbody;
    private Animator animator;
    private bool facingRight = true; // Œ»İ‚ÌŒü‚«‚ğŠÇ—

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 predictedPos = PredictPlayerPosition();
        float distance = Vector2.Distance(transform.position, predictedPos);

        if (distance > attackRange)
        {
            // —\‘ªˆÊ’u‚ÉŒü‚©‚Á‚ÄˆÚ“®
            Vector2 direction = (predictedPos - (Vector2)transform.position).normalized;
            rbody.velocity = new Vector2(direction.x * moveSpeed, rbody.velocity.y);

            // is•ûŒü‚ğŒü‚­
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            // UŒ‚
            Debug.Log("—\‘ª’n“_‚ÉUŒ‚I");

            // ˆÚ“®‚ğ~‚ß‚é
            rbody.velocity = new Vector2(0, rbody.velocity.y);
        }

        // === ƒAƒjƒ[ƒVƒ‡ƒ“”»’è ===
        if (Mathf.Abs(rbody.velocity.y) > 0.01f)
        {
            // ‹ó’†‚É‚¢‚é
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        else if (Mathf.Abs(rbody.velocity.x) > 0.01f)
        {
            // ’n–Ê‚ÅˆÚ“®’†
            animator.SetBool("Jump", false);
            animator.SetBool("Run", true);
        }
        else
        {
            // ’n–Ê‚Å’â~’†
            animator.SetBool("Jump", false);
            animator.SetBool("Run", false);
        }
    }

    Vector2 PredictPlayerPosition()
    {
        return (Vector2)player.position + playerRb.velocity * predictionTime;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // ¶‰E”½“]
        transform.localScale = scale;
    }
}
