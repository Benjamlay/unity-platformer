using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class FallDetector : MonoBehaviour
{
    [SerializeField] GameObject player;
    private bool _isRespawning = false;
    
    public Transform respawnPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Respawn");
        }
        RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        _isRespawning = true;
        player.transform.position = respawnPoint.position;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        _isRespawning = false; 
    }
}
