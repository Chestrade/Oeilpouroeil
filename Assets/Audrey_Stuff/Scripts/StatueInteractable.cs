using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StatueInteractable : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject interactPromptHint;

    public string statueText;

    public Transform eyeTransform;

    private NoteManager noteManager;

    private CollectedEyeManager collectedEyeManager;
    private CollectedStatueManager collectedStatueManager;
    public int statueID;
    private EndingPortal endingPortal;

    // Start is called before the first frame update
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        interactPrompt.SetActive(false);   //When the game start, the prompt is turned off
        interactPromptHint.SetActive(false);
        collectedEyeManager = FindObjectOfType<CollectedEyeManager>();
        collectedStatueManager = FindObjectOfType<CollectedStatueManager>();
        endingPortal = FindObjectOfType<EndingPortal>();
    }


    public void PlayerEnterProximity()
    {
        interactPrompt.SetActive(true);
    }
    public void PlayerExitProximity()
    {
        interactPrompt.SetActive(false);
    }

    public void PlayerEnterProximityHint()
    {
        interactPromptHint.SetActive(true);
    }
    public void PlayerExitProximityHint()
    {
        interactPromptHint.SetActive(false);
    }

    public void PlaceEye()
    {
        noteManager.noteTextMesh.text = statueText;
        noteManager.EnableTextBox();
        collectedEyeManager.DisableEye();
        collectedStatueManager.statueID = statueID;
        collectedStatueManager.EnableStatue();
        endingPortal.PortaleEnabled();
        PlayerPrefs.SetInt("Statue" + statueID, 1);

    }
    public void CloseNote()
    {
        noteManager.DisableTextBox();
    }
}
