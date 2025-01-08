using UnityEngine;

public class laddersContact : MonoBehaviour
{
    private bool _contactladderDetected = false;
    public bool _ContactLadderDetected => _contactladderDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladders"))
        {
            _contactladderDetected = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladders"))
        {
            _contactladderDetected = false;
        }
        
    }
    

}
