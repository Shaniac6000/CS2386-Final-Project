using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    private LevelManager lm;
    private DialogueManager dm;
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        dm = FindFirstObjectByType<DialogueManager>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gnome"))
        {
            dm.StartDialogue("end_of_level");
            lm.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
