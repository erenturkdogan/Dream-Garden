using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject spikePrefab;       // Diken prefabı
    public Transform firePoint;          // Diken çıkış noktası
    public float attackInterval = 2f;    // Dalga arası süre
    public int spikesPerWave = 5;        // Dalga başına kaç diken
    public float spreadAngle = 45f;      // Dikenlerin açısal dağılımı

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            ShootWave();
        }
    }

    void ShootWave()
    {
        if (player == null) return;

        // Player yönünü hesapla
        Vector2 dirToPlayer = (player.position - firePoint.position).normalized;
        float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

        // Dalga başına spikesPerWave kadar diken
        for (int i = 0; i < spikesPerWave; i++)
        {
            // Dikenin açısı (spread içinde eşit dağılım)
            float angleOffset = Mathf.Lerp(-spreadAngle / 2, spreadAngle / 2, i / (float)(spikesPerWave - 1));
            float finalAngle = baseAngle + angleOffset;
            Vector2 dir = new Vector2(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad));

            // Diken spawn
            GameObject spike = Instantiate(spikePrefab, firePoint.position, Quaternion.identity);
            spike.GetComponent<BossSpike>().SetDirection(dir);
        }
    }
}
