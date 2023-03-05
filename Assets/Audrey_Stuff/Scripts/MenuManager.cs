using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject blackFade;
    public GameObject startMenuSettings;
    public GameObject optionsScreen;
    public GameObject creditsScreen;

    void Start()
    {
        blackFade.SetActive(false);
        optionsScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }


    public void StartGame()
    {
        StartCoroutine(StartDelay());
    }

    public void Options()
    {
        startMenuSettings.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void Credits()
    {
        startMenuSettings.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void ReturnToStart()
    {
        startMenuSettings.SetActive(true);
        optionsScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    IEnumerator StartDelay()
    {
        blackFade.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        {
            SceneManager.LoadScene(1);
            //Loads first scene set in Build Index
        }
    }
}
