using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    public GameObject interactPrompt;

    public string noteText;

    public NoteManager noteManager;

    [Header("Wwise Stuff")]
    public AK.Wwise.Event noteOpenEvent;
    public AK.Wwise.Event noteCloseEvent;

    // Start is called before the first frame update
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        interactPrompt.SetActive(false);   //When the game start, the prompt is turned off
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerEnterProximity()
    {
        interactPrompt.SetActive(true);
    }
    public void PlayerExitProximity()
    {
        interactPrompt.SetActive(false);
    }

    public void OpenNote()
    {
        noteManager.noteTextMesh.text = noteText;
        noteManager.EnableTextBox();
        noteOpenEvent.Post(gameObject);
    }

    public void CloseNote()
    {
        noteManager.DisableTextBox();
        noteCloseEvent.Post(gameObject);
    }
}
