using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 10f;
    public int maxJumps = 2;

    private float currentSpeed;
    private int jumpCount;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        currentSpeed = speed;
        jumpCount = maxJumps;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        bool run = Keyboard.current.leftShiftKey.isPressed;
        currentSpeed = run ? runSpeed : speed;

        float moveX = 0f;
        if (Keyboard.current.aKey.isPressed) moveX = -1;
        if (Keyboard.current.dKey.isPressed) moveX = 1;

        rb.linearVelocity = new Vector2(moveX * currentSpeed, rb.linearVelocity.y);

        if (moveX > 0) spriteRenderer.flipX = false;
        if (moveX < 0) spriteRenderer.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(moveX) * (run ? 2f : 1f));

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = maxJumps;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            float hitY = collision.contacts[0].normal.y;

            // ÜSTÜNE BASMA → ENEMY ÖLÜR
            if (hitY < -0.5f)
            {
                Enemy enemy = collision.collider.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.Die();

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce / 1.5f);
            }
            else
            {
                // YANDAN → PLAYER HASAR
                playerHealth.TakeDamage();
            }
        }
    }
}
