using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelGoal : MonoBehaviour
{
    private LevelManager lm;
    private DialogueManager dm;
    public float delayTime;

    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        dm = FindFirstObjectByType<DialogueManager>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gnome"))
        {
           StartCoroutine(PlayDialogueThenLoad());
        }
    }

    IEnumerator PlayDialogueThenLoad()
    {
        dm.StartDialogue("end_of_level");
        yield return new WaitForSeconds(delayTime);
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            lm.LoadLevel(nextIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
