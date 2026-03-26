using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;

    void Start()
    {
        panel.SetActive(false);
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
