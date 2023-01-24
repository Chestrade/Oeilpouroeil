using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour
{
    public bool debugPlayerPrompt;
    public bool debugPlayerInteract;

    public GameObject interactPrompt;

    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.SetActive(false);   //When the game start, the prompt is turned off
    }

    // Update is called once per frame
    void Update()
    {
        if (debugPlayerPrompt == true)  //To be replaced with player proximity (however we make it)
        {
            interactPrompt.SetActive(true);
        }

        else
        {
            interactPrompt.SetActive(false);
        }

    }
}
