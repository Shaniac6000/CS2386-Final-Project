using UnityEngine;
using Ink.Runtime;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
     public TextAsset inkJSONAsset;
     public DialogueUI dialogueUI;

    public Story story;

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
            StartCoroutine(AutoContinue());
        }
        else
        {
            StartCoroutine(HideAfterDelay());
        }
    }

    private IEnumerator AutoContinue()
    {
        yield return new WaitForSeconds(12f);
        ContinueStory();
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        dialogueUI.Hide();
    }

}
