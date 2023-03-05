using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public GameObject interactPrompt;
    public bool rocksPushed;

    public Transform rockFallTransform;
    public AudioSource rockFallSFX;
    void Start()
    {
    //    interactPrompt.SetActive(false);
    }

    private void Update()
    {
        if(rocksPushed)
        {
            transform.position = Vector3.Lerp(transform.position, rockFallTransform.position, 2 * Time.deltaTime);
        }
    }

    public void PushRocks()
    {
        rocksPushed = true;
        print("WEEEEEE");

        //Play SFX
        rockFallSFX.Play();
        interactPrompt.SetActive(false);
    }

    public void PlayerEnterProximity()
    {
        interactPrompt.SetActive(true);
    }
    public void PlayerExitProximity()
    {
        interactPrompt.SetActive(false);
    }
}
