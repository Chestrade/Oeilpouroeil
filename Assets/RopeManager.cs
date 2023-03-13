using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    private bool ropeInProximity;
    private bool pullRopeToggle;
    private RopePull ropeInteractable;

    public Transform ropeHolder;
    public float pullThreshold = 1.5f;

    void Start()
    {
        ropeInteractable = FindObjectOfType<RopePull>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pullRopeToggle = true;

            if (ropeInProximity == true && ropeInteractable.ropePulled == true && pullRopeToggle == true)
            {
                pullRopeToggle = false;

                ropeInteractable.gameObject.transform.parent = null;
                ropeInteractable.DropRope();
            }

            // Picks Up rope
            if (ropeInProximity == true && ropeInteractable.ropePulled == false && pullRopeToggle == true)
            {
                pullRopeToggle = false;

                ropeInteractable.gameObject.transform.parent = ropeHolder;
                ropeInteractable.gameObject.transform.position = ropeHolder.position;
                ropeInteractable.PullRope();

            }



        }

        if(ropeInteractable.ropePulled)
        {
            ropeInteractable.transform.position = ropeHolder.position;

            ropeInteractable.FindPullDistance();

            if(ropeInteractable.pullDistance > pullThreshold)
            {
                pullRopeToggle = false;
                ropeInteractable.gameObject.transform.parent = null;
                ropeInteractable.TriggerAction(); //TRIGGERS ASSOCIATED ACTION
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            ropeInteractable = other.GetComponent<RopePull>();
            ropeInteractable.PlayerEnterProximity();
            ropeInProximity = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            ropeInteractable.PlayerExitProximity();
            ropeInProximity = false;

        }
    }
}
