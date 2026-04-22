using TMPro;
using UnityEngine;

public class CageOpen : MonoBehaviour
{
    private bool unlocked;
    private static int gnomesFreed;
    public TextMeshProUGUI gnomeCounter;
    private DialogueManager dm;

    void Start()
    {
        gnomesFreed = 1;
        dm = FindFirstObjectByType<DialogueManager>();
    } 

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {   
            transform.parent.transform.localRotation = Quaternion.Lerp(transform.parent.transform.localRotation, Quaternion.Euler(0, -90, 0), .5f * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable") && collision.gameObject.layer == LayerMask.NameToLayer("Key"))
        {
            GetComponent<AudioSource>().Play();
            unlocked = true;
            Destroy(collision.gameObject);
            gnomesFreed++;
            gnomeCounter.text = "Gnomies: " + gnomesFreed;
           
            dm.story.variablesState["first_gnome_saved"] = true;
            if(gnomesFreed > 2)
            {
                dm.story.variablesState["first_gnome_saved"] = false;
            }
                dm.StartDialogue("open_cage");
        }
    }
}
