using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    private bool pauseGame = false;
    public GameObject pauseScreen;

    void Start()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if(pauseGame == true)
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Unpause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        pauseGame = false;
    }
}
