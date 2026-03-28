using TMPro;
using UnityEngine;

public class TrappedGnome : MonoBehaviour
{
    public TMP_Text gnomeCount;
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
            gnomeCount.text = "Gnomies: 2";
            
            //cue the dialogue
            var dm = FindObjectOfType<DialogueManager>();
            dm.StartDialogue("soil_bag_removed");
        }
    }
}
