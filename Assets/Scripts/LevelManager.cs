using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int maxLevel = 1;

    public void LoadLevel(int buildIndex)
    {
        if (buildIndex > maxLevel)
        {
            maxLevel = buildIndex;
        }
        //cue the dialogue
            //var dm = FindFirstObjectByType<DialogueManager>();
            //dm.StartDialogue("end_of_level");
    
        SceneManager.LoadScene(buildIndex);
    }
}
