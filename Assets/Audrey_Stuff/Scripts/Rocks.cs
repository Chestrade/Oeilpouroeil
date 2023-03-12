using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public GameObject interactPrompt;
    public bool rocksPushed;

    public Transform[] rockFallTransform;
    public AK.Wwise.Event rockFallEvent;
    //public AudioSource rockFallSFX;

    private bool rocksPushStep1;
    private bool rocksPushStep2;

    void Start()
    {
        interactPrompt.SetActive(false);
    }

    private void Update()
    {
        if(rocksPushStep1)
        {
            transform.position = Vector3.Lerp(transform.position, rockFallTransform[0].position, 1.5f * Time.deltaTime);
        }
        if (rocksPushStep2)
        {
            transform.position = Vector3.Lerp(transform.position, rockFallTransform[1].position, 2f * Time.deltaTime);
        }
    }

    public void PushRocks()
    {
        rocksPushed = true;
        StartCoroutine(PushDelay());

        //Play SFX
        //rockFallSFX.Play();
        rockFallEvent.Post(gameObject);
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

    IEnumerator PushDelay()
    {
        rocksPushStep1 = true;
        yield return new WaitForSeconds(1f);
        {
            rocksPushStep1 = false;
            rocksPushStep2 = true;

            yield return new WaitForSeconds(1.5f);
            {
                rocksPushStep2 = false;
            }
        }
    }


    public void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, rockFallTransform[0].position, Color.green);
        Debug.DrawLine(rockFallTransform[0].position, rockFallTransform[1].position, Color.green);
    }
}
