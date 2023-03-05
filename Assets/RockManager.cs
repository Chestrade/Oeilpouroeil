using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public bool rocksInProximity;

    private Rocks rockToPush;

    // Start is called before the first frame update
    void Start()
    {
        rockToPush = FindObjectOfType<Rocks>();
    }

    // Update is called once per frame
    void Update()
    {
        // Opens the note
        if (rocksInProximity == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (rockToPush.rocksPushed == false)
                {
                    rockToPush.PushRocks();
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            rockToPush = other.GetComponent<Rocks>();

            if(rockToPush.rocksPushed == false)
            {
                rockToPush.PlayerEnterProximity();
                rocksInProximity = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            if (rockToPush.rocksPushed == false)
            {
                rockToPush.PlayerExitProximity();
                rocksInProximity = false;
            }
        }
    }
}
