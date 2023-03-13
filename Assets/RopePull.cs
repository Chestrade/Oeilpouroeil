using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePull : MonoBehaviour
{
    public GameObject interactPrompt;

    public bool ropePulled = false;
    public bool triggerAction = false;

    public Transform ropeBasePos;
    public Transform ropePullPos;

    public float pullDistance;

    void Start()
    {
        interactPrompt.SetActive(false);   //When the game start, the prompt is turned off
    }

    public void FindPullDistance()
    {
        pullDistance = Vector3.Distance(ropeBasePos.position, ropePullPos.position);
    }


    public void PlayerEnterProximity()
    {
        if (ropePulled == false)
        {
            interactPrompt.SetActive(true);
        }
    }
    public void PlayerExitProximity()
    {
        if (ropePulled == false)
        {
            interactPrompt.SetActive(false);
        }
    }
    public void PullRope()
    {
        ropePulled = true;
        interactPrompt.SetActive(false);

    }

    public void DropRope()
    {
        ropePulled = false;
    }

    public void TriggerAction()
    {
        triggerAction = true;
        ropePulled = false;
        StartCoroutine(ActionDisableDelay());
    }



    IEnumerator ActionDisableDelay()
    {
        yield return new WaitForSeconds(0.00001f);
        {
            triggerAction = false;
        }
    }
}
