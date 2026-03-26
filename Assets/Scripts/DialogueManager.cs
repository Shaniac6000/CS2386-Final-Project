using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
     public TextAsset inkJSONAsset;
     public DialogueUI dialogueUI;

    private Story story;

    void Awake()
    {
        story = new Story(inkJSONAsset.text);
    }
    
   public void StartDialogue(string knot)
    {
        story.ChoosePathString(knot);
        ContinueStory();
    }

    public void ContinueStory()
    {
        if (story.canContinue)
        {
            string line = story.Continue();
            dialogueUI.Show(line);
        }
        else
        {
            dialogueUI.Hide();
        }
    }

}
