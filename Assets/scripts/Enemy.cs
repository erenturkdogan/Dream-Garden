using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer sr; // SpriteRenderer referansı

    public Color hitColor = Color.red;   // Hasar alındığında renk
    public float flashDuration = 0.1f;   // Kaç saniye kalacak

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            Debug.LogError("SpriteRenderer bulunamadı!");
    }

    public void TakeDamage(int damage = 1)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " hit! Remaining health: " + currentHealth);

        // 🔹 Görsel efekt: kısa süre kırmızı
        if (sr != null)
            StartCoroutine(Flash());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator Flash()
    {
        sr.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = Color.white; // eski renk
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " DIED");
        Destroy(gameObject);
    }
}
