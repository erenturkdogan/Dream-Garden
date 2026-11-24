using UnityEngine;

public class BossSpike : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 1;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player'a çarparsa
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage();
            }

            Destroy(gameObject);
        }

        // Zemin / duvar
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
