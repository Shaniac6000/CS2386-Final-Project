using UnityEngine;

public class TrappedGnome : MonoBehaviour
{
    public bool trapped = true;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrappedTrigger"))
        {
            trapped = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TrappedTrigger"))
        {
            trapped = false;
        }
    }
}
