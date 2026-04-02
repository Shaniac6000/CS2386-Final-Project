using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int maxLevel = 1;

    void Start()
    {
        maxLevel = PlayerPrefs.GetInt("levelReached", 1);
    }
    public void LoadLevel(int buildIndex)
    {
        if (buildIndex > maxLevel)
        {
            maxLevel = buildIndex;
            PlayerPrefs.SetInt("levelReached", maxLevel);
        }
    
        SceneManager.LoadScene(buildIndex);
    }
}
