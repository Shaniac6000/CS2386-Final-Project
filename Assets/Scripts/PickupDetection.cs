using TMPro;
using UnityEngine;

public class PickupDetection : MonoBehaviour
{
    public TextMeshProUGUI grabIndicator;
    private GameObject target = null;
    private bool trapped = false;

    void Start()
    {
        if (transform.parent.gameObject.GetComponent<TrappedGnome>())
        {
            trapped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Throwable") || other.CompareTag("Draggable")) && !trapped)
        {
            target = other.gameObject;
            grabIndicator.text = "Press E to Grab";
            grabIndicator.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Throwable") || other.CompareTag("Draggable"))
        {
            target = null;
            grabIndicator.enabled = false;
        }
    }

    public GameObject getTarget()
    {
        return target;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public void Untrap()
    {
        trapped = false;
    }
}
