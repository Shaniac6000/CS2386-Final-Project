using TMPro;
using UnityEngine;

public class TrappedGnome : MonoBehaviour
{
    public TMP_Text gnomeCount;
    public bool trapped = true;
    public PickupDetection pd;

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
            pd.Untrap();
            //cue the dialogue
            var dm = FindFirstObjectByType<DialogueManager>();
            dm.StartDialogue("soil_bag_removed");
        }
    }
}
