using UnityEngine;

public class SeedPickup : MonoBehaviour
{
    public int seedAmount =1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SeedBarController seedBar = FindObjectOfType<SeedBarController>();
            seedBar.AddSeed(seedAmount);
            Destroy(gameObject);
        }
    }
}
