using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingPortal : MonoBehaviour
{
    public GameObject portalEffects;
    public int levelToLoad = 1;
    public Collider myCollider;
    public AK.Wwise.Event portalLoopTrigger;

    void Start()
    {
        portalEffects.SetActive(false);
        myCollider.enabled = false;
    }

    public void PortaleEnabled()
    {
        portalEffects.SetActive(true);
        myCollider.enabled = true;
        portalLoopTrigger.Post(gameObject);
        //Debug.Log("Portal Activated");
    }



}
