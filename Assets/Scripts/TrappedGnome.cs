using TMPro;
using UnityEngine;

public class TrappedGnome : MonoBehaviour
{
    public TMP_Text gnomeCount;
    public bool trapped = true;
    public PickupDetection pd;
    public Animator animator;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TrappedTrigger") && trapped)
        {
            trapped = false;
            gnomeCount.text = "Gnomies: 2";
            pd.Untrap();
            animator.SetTrigger("Untrap");
            
            //cue the dialogue
            var dm = FindFirstObjectByType<DialogueManager>();
            dm.StartDialogue("soil_bag_removed");
        }
    }
}
