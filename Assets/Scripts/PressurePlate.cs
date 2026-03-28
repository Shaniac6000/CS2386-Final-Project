using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome"))
        {
            isPressed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome"))
        {
            isPressed = false;
        }
    }
}
