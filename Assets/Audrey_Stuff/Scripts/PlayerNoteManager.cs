using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoteManager : MonoBehaviour
{

    public bool noteInProximity;

    public bool openNote;

    private Note noteToRead;

    // Start is called before the first frame update
    void Start()
    {
        noteToRead = GetComponent<Note>();
    }

    // Update is called once per frame
    void Update()
    {
        // Opens the note
        if (noteInProximity == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                openNote = !openNote;

                if (openNote == true)
                {
                    noteToRead.OpenNote();
                    print("Open note");
                }
                if (openNote == false)
                {
                    noteToRead.CloseNote();
                    print("Close note");

                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            noteToRead = other.GetComponent<Note>();
            noteToRead.PlayerEnterProximity();
            noteInProximity = true;
            //print("Note Proximity");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            noteToRead = other.GetComponent<Note>();
            noteToRead.PlayerExitProximity();
            noteInProximity = false;
            noteToRead.CloseNote();
            openNote = false;
        }
    }
}
