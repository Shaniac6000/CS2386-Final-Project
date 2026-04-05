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
            grabIndicator.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!trapped && !transform.parent.GetComponent<GnomeController>().carrying)
        {
            if (other.CompareTag("Throwable") || other.CompareTag("Draggable"))
            {
                target = other.gameObject;
                grabIndicator.text = "Press E to Grab";
                grabIndicator.enabled = true;
            }

            if (other.name != transform.parent.name && other.CompareTag("Gnome") && !(other.GetComponent<TrappedGnome>() && other.GetComponent<TrappedGnome>().trapped))
            {
                target = other.gameObject;
                grabIndicator.text = "Press E to Stack";
                grabIndicator.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Throwable") || other.CompareTag("Draggable") || other.CompareTag("Gnome"))
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
