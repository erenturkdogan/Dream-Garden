using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int health = 3;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    private bool isDead = false;

    [Header("Damage Effect")]
    public Image damageOverlay;
    public float flashDuration = 0.2f;
    public float maxAlpha = 0.5f;

    [Header("Game Over")]
    public GameOverManager gameOverManager; // 👈 eklendi

    void Start()
    {
        UpdateHearts();

        if (damageOverlay != null)
        {
            Color c = damageOverlay.color;
            c.a = 0;
            damageOverlay.color = c;
        }
    }

    // 🔹 NORMAL HASAR (1 CAN)
    public void TakeDamage()
    {
        if (isDead) return;

        health--;
        UpdateHearts();

        if (damageOverlay != null)
            StartCoroutine(DamageFlash());

        if (health <= 0)
        {
            Die();
        }
    }

    // 🔥 VOID / BOŞLUK / TEK SEFERDE ÖLÜM
    public void InstantDeath()
    {
        if (isDead) return;

        health = 0;
        UpdateHearts();
        Die();
    }

    // 🔹 Kalpler
    void UpdateHearts()
    {
        if (heart1 != null) heart1.enabled = health >= 1;
        if (heart2 != null) heart2.enabled = health >= 2;
        if (heart3 != null) heart3.enabled = health >= 3;
    }

    // 🔹 Ölüm
    void Die()
    {
        isDead = true;
        Debug.Log("PLAYER DIED");

        // 🎮 Game Over ekranını aç
        if (gameOverManager != null)
            gameOverManager.ShowGameOver();
        else
            Debug.LogError("GameOverManager atanmadı!");

        gameObject.SetActive(false); // player sahneden kaybolur
    }

    // 🔹 Ekran kırmızı flash
    IEnumerator DamageFlash()
    {
        Color c = damageOverlay.color;
        c.a = maxAlpha;
        damageOverlay.color = c;

        yield return new WaitForSeconds(flashDuration);

        c.a = 0;
        damageOverlay.color = c;
    }
}
