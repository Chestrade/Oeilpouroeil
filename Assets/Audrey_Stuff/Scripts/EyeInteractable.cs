using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeInteractable : MonoBehaviour
{
    public GameObject interactPrompt;

    public CollectedEyeManager collectedEyeManager;

    public bool eyePickedUp = false;

    public int eyeID;

    public AK.Wwise.Event crystalGlowLoop;

    private Rigidbody rb;

    void Start()
    {
        collectedEyeManager = FindObjectOfType<CollectedEyeManager>();
        interactPrompt.SetActive(false);   //When the game start, the prompt is turned off
        crystalGlowLoop.Post(gameObject);
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
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
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    public void DropEye()
    {
        eyePickedUp = false;
        interactPrompt.SetActive(true);

        //Play sound fx, etc here
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
