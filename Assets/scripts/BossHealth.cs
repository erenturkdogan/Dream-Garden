using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 20;
    private int currentHealth;

    [Header("UI")]
    public Slider bossHealthBar;

    [Header("Hit Effect")]
    public SpriteRenderer sr;
    public Color hitColor = Color.red;
    public float flashTime = 0.1f;

    [Header("Portal")]
    public GameObject portal;  // Boss öldüğünde aktif olacak portal

    void Start()
    {
        currentHealth = maxHealth;

        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        // Can barı başlat
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.maxValue = maxHealth;
            bossHealthBar.value = currentHealth;
        }

        // Portal başlangıçta kapalı
        if (portal != null)
            portal.SetActive(false);
    }

    // Boss hasar alır
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (bossHealthBar != null)
            bossHealthBar.value = currentHealth;

        StartCoroutine(Flash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Vurulma efekti
    IEnumerator Flash()
    {
        sr.color = hitColor;
        yield return new WaitForSeconds(flashTime);
        sr.color = Color.white;
    }

    // Boss ölünce
    void Die()
    {
        Debug.Log("BOSS DEAD");

        // Can barı kaybolur
        if (bossHealthBar != null)
            bossHealthBar.gameObject.SetActive(false);

        // Portal açılır
        if (portal != null)
            portal.SetActive(true);

        Destroy(gameObject);
    }
}
