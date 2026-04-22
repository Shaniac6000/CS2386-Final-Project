using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public LockBehavior lb;
    public PressurePlate p1;
    public PressurePlate p2;
    private bool opened, dialogue;
    private DialogueManager dm;
    public AudioClip sfx;

    void Start()
    {
        dm = FindFirstObjectByType<DialogueManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!lb && p1.isPressed && p2.isPressed)
        {
            opened = true;
        }

        else if (!lb.locked && p1.isPressed && p2.isPressed)
        {
            opened = true;
        }

        if (opened)
        {
            
                door1.rotation = Quaternion.Lerp(door1.rotation, Quaternion.Euler(0, 55f, 0), .5f * Time.deltaTime);
                door2.rotation = Quaternion.Lerp(door2.rotation, Quaternion.Euler(0,-55, 0), .5f * Time.deltaTime);
            //cue the dialogue
            if (!dialogue)
            {
                dm.StartDialogue("open_door");
                if (sfx)
                {
                    AudioSource.PlayClipAtPoint(sfx, transform.position);
                }
                dialogue = true;
            }
        }
    }
}
