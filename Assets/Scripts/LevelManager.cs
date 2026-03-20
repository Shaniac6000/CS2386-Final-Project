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
        SceneManager.LoadScene(buildIndex);
    }
}
