using UnityEngine;

public class PortalController : MonoBehaviour
{
    private bool playerInside = false;

    [Header("Win Manager")]
    public WinManager winManager;  // Inspector'dan bağla

    void Update()
    {
        if (playerInside && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (winManager != null)
            {
                winManager.ShowWin();
            }
            else
            {
                Debug.LogError("WinManager atanmadı!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
