using UnityEngine;

public class PickupDetection : MonoBehaviour
{
    private GameObject target = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            target = null;
        }
    }

    public GameObject getTarget()
    {
        return target;
    }
}
