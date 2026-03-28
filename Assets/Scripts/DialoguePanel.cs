using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;

    void Start()
    {
        var dm = FindFirstObjectByType<DialogueManager>();
        dm.StartDialogue("on_load");
    }

    public void Show(string line)
    {
        panel.SetActive(true);
        text.text = line;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
