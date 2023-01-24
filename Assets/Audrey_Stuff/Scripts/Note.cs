using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    public bool debugPlayerPrompt;
    public bool debugPlayerInteract;

    public GameObject interactPrompt;

    public string noteText;

    public NoteManager noteManager;

    private bool closeNoteToggle;

    // Start is called before the first frame update
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
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


        if (debugPlayerPrompt == true && debugPlayerInteract == true)
        {
            noteManager.noteTextMesh.text = noteText;
            noteManager.EnableTextBox();
            closeNoteToggle = true;
        }

        if (debugPlayerInteract == false && closeNoteToggle == true)   
        {
            noteManager.DisableTextBox();
            closeNoteToggle = false;
        }
    }
}
