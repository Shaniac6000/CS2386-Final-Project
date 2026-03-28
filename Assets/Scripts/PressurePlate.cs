using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed;
    private int gnomeCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gnomeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gnomeCount >= 2)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome"))
        {
            gnomeCount++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome"))
        {
            gnomeCount--;
        }
    }
}
