using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;
    private DialogueManager dm;

    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome"))
        {
            isPressed = true;
            dm.StartDialogue("reach_pressure_plates");
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
