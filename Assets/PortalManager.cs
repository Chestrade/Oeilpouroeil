using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    private EndingPortal endingPortal;
    public GameObject blackFade;

    void Start()
    {
        endingPortal = FindObjectOfType<EndingPortal>();
        blackFade = GameObject.FindGameObjectWithTag("EndingFade");
        blackFade.SetActive(false);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            StartCoroutine(LoadDelay());
            print("Load Level " + endingPortal.levelToLoad);
        }
    }



    IEnumerator LoadDelay()
    {
        blackFade.SetActive(true);
        yield return new WaitForSeconds(2f);
        {
            SceneManager.LoadScene(endingPortal.levelToLoad);
        }
    }
}
