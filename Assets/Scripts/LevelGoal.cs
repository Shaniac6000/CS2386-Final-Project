using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    private LevelManager lm;
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gnome"))
        {
            lm.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
