using UnityEngine;
using UnityEngine.UI;

public class SeedBarController : MonoBehaviour
{
    public Slider seedBar;
    public int maxSeed = 5;
    private int currentSeed = 0;

    public GameObject portal; // 👈 EKLENDİ

    void Start()
    {
        seedBar.maxValue = maxSeed;
        seedBar.value = currentSeed;
    }

    public void AddSeed(int amount)
    {
        currentSeed += amount;
        currentSeed = Mathf.Clamp(currentSeed, 0, maxSeed);
        seedBar.value = currentSeed;

        // 👇 SEED FULL OLUNCA
        if (currentSeed >= maxSeed)
        {
            portal.SetActive(true);
        }
    }
}
