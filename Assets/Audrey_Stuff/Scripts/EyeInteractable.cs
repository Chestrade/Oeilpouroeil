using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeInteractable : MonoBehaviour
{
    public GameObject interactPrompt;

    public CollectedEyeManager collectedEyeManager;

    public bool eyePickedUp = false;

    public int eyeID;


    void Start()
    {
        collectedEyeManager = FindObjectOfType<CollectedEyeManager>();
        interactPrompt.SetActive(false);   //When the game start, the prompt is turned off
    }


    public void PlayerEnterProximity()
    {
        if (eyePickedUp == false)
        {
            interactPrompt.SetActive(true);
        }
    }
    public void PlayerExitProximity()
    {
        if (eyePickedUp == false)
        {
            interactPrompt.SetActive(false);
        }
    }



    public void PickUpEye()
    {
        //gameObject.SetActive(false);
        collectedEyeManager.eyeID = eyeID;
        collectedEyeManager.EnableEye();
        eyePickedUp = true;
        interactPrompt.SetActive(false);

        //Play sound fx, etc here

    }
}
