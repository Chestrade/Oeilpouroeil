using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeInteractableManager : MonoBehaviour
{
    public bool eyeInProximity;
    public bool statueInProximity;

    public bool pickUpEye;

    public EyeInteractable eyeInteractable;
    public StatueInteractable statueInteractable;

    public int currentlyHeldEye;

    public Transform eyeHolder;

    void Start()
    {
        eyeInteractable = FindObjectOfType<EyeInteractable>();
        statueInteractable = FindObjectOfType<StatueInteractable>();

    }

    // Update is called once per frame
    void Update()
    {
        // Picks Up eye
        if (eyeInProximity == true && eyeInteractable.eyePickedUp == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                eyeInteractable.PickUpEye();
                print("PickUpEye");
                currentlyHeldEye = eyeInteractable.eyeID;

                eyeInteractable.gameObject.transform.parent = eyeHolder;
                eyeInteractable.gameObject.transform.position = eyeHolder.position;
                eyeInteractable.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }

        // Places the eye
        if (statueInProximity == true && eyeInteractable.eyePickedUp == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                statueInteractable.PlaceEye();
                print("PickUpEye");

                eyeInteractable.gameObject.transform.parent = statueInteractable.eyeTransform;
                eyeInteractable.gameObject.transform.position = statueInteractable.eyeTransform.position;
                eyeInteractable.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Eye"))
        {
            eyeInteractable = other.GetComponent<EyeInteractable>();
            eyeInteractable.PlayerEnterProximity();
            eyeInProximity = true;
            //print("Note Proximity");
        }

        if (other.gameObject.CompareTag("Statue"))
        {
            if (eyeInteractable.eyePickedUp == true)
            {
                statueInteractable = other.GetComponent<StatueInteractable>();
                statueInteractable.PlayerEnterProximity();
                statueInProximity = true;
            }
            else
            {
                statueInteractable = other.GetComponent<StatueInteractable>();
                statueInteractable.PlayerEnterProximityHint();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Eye"))
        {
            eyeInteractable.PlayerExitProximity();
            eyeInProximity = false;
        }

        if (other.gameObject.CompareTag("Statue"))
        {
            if (eyeInteractable.eyePickedUp == true)
            {
                statueInteractable.PlayerExitProximity();
                statueInProximity = false;
                statueInteractable.CloseNote();
            }
            else
            {
                statueInteractable = other.GetComponent<StatueInteractable>();
                statueInteractable.PlayerExitProximityHint();
            }
        }
    }
}
