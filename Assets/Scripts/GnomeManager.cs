using TMPro;
using UnityEngine;

public class GnomeManager : MonoBehaviour
{
    public GameObject activeGnome;
    private GameObject[] gnomes;
    private int activeIndex;
    private CameraFollow cameraFollow;
    public TextMeshProUGUI grabIndicator;
    
    void Start()
    {
        cameraFollow = FindAnyObjectByType<CameraFollow>();
        gnomes = GameObject.FindGameObjectsWithTag("Gnome");
        if (gnomes.Length == 0)
        {
            Debug.Log("Error: No gnomes found in scene");
        }
        
        if (!activeGnome)
        {
            ActivateGnome(gnomes[0], 0);
        }
        else
        {
            cameraFollow.SetTarget(activeGnome.transform);
            activeIndex = GetGnomeIndex(activeGnome);
            activeGnome.GetComponent<GnomeController>().isActive = true;
            activeGnome.GetComponent<CharacterController>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextGnome();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevGnome();
        }
    }

    void ActivateGnome(GameObject gnome, int index)
    {
        cameraFollow.SetTarget(gnome.transform);
        activeGnome = gnome;
        activeIndex = index;
        activeGnome.GetComponent<GnomeController>().isActive = true;
        activeGnome.GetComponent<GnomeController>().thrown = false;
        //activeGnome.GetComponent<CharacterController>().enabled = true;
        activeGnome.GetComponentInChildren<PickupDetection>().enabled = true;
    }

    void DeactivateCurrentGnome()
    {
        activeGnome.GetComponent<GnomeController>().isActive = false;
        //activeGnome.GetComponent<CharacterController>().enabled = false;
        activeGnome.GetComponentInChildren<PickupDetection>().ClearTarget();
        activeGnome.GetComponentInChildren<PickupDetection>().enabled = false;
    }

    // find index of given gnome object in gnomes array
    int GetGnomeIndex(GameObject gnome)
    {
        for (int idx = 0; idx < gnomes.Length; idx++)
        {
            if (gnomes[idx] == gnome)
            {
                return idx;
            }
        }
        return -1;
    }

    private void NextGnome()
    {
        int newIndex;
        if (activeIndex == gnomes.Length - 1)
        {
            newIndex = 0;
        }
        else
        {
            newIndex = activeIndex + 1;
        }
        if (gnomes[newIndex].GetComponent<TrappedGnome>() && gnomes[newIndex].GetComponent<TrappedGnome>().trapped)
        {
            activeIndex = newIndex;
            NextGnome();
        }
        else
        {
            if (gnomes[newIndex].GetComponent<TrappedGnome>())
            {
                gnomes[newIndex].GetComponent<TrappedGnome>().enabled = false;
            }
            DeactivateCurrentGnome();
            ActivateGnome(gnomes[newIndex], newIndex);
            grabIndicator.enabled = false;
        }
    }
    
    private void PrevGnome()
    {
        int newIndex;
        if (activeIndex == 0)
        {
            newIndex = gnomes.Length - 1;
        }
        else
        {
            newIndex = activeIndex - 1;
        }
        if (gnomes[newIndex].GetComponent<TrappedGnome>() && gnomes[newIndex].GetComponent<TrappedGnome>().trapped)
        {
            activeIndex = newIndex;
            NextGnome();
        }
        else
        {
            if (gnomes[newIndex].GetComponent<TrappedGnome>())
            {
                gnomes[newIndex].GetComponent<TrappedGnome>().enabled = false;
            }
            DeactivateCurrentGnome();
            ActivateGnome(gnomes[newIndex], newIndex);
            grabIndicator.enabled = false;
        }
    }

}
