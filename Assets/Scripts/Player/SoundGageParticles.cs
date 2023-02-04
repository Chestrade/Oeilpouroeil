using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGageParticles : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem quietRipples;
    [SerializeField] private ParticleSystem loudRipples;

    [Header("Particle Timing")]
    [SerializeField] private float timeBetweenRipples;
    [SerializeField] private float timeBetweenRibbits;
    
    //private ParticleSystem currentRipples;

    private PlayerController player;

    private void Start()
    {
        player = PlayerController.instance;
        //currentRipples = null;
    }

    private void Update()
    {
        
        if (Input.GetButton("Fire1"))
        {
            //Ribbit();
            StartCoroutine(Ribbit());
        }
        

    }
    private void QuietRipple()
    {
        StartCoroutine(QuietRippleCoroutine());
        /*
        //currentRipples = quietRipples;
        if(loudRipples.isPlaying)
        {
            loudRipples.Stop();
        }
        if (player.isIdle == false) 
        {
            quietRipples.Play();
            Debug.Log("Quiet Step");
        }
        if(player.isIdle == true || player.grounded == false)
        {
            quietRipples.Stop();
        }
        else
        {
            // Debug.Log("Why are the quiet ripples playing?");
            return;
        }
        */

    }

    private void LoudRipple()
    {
        StartCoroutine(LoudRippleCoroutine());
        /*
        //currentRipples = loudRipples;
        if(quietRipples.isPlaying)
        {
            quietRipples.Stop();
        }
        if (player.isIdle == false && player.grounded)
        {
            loudRipples.Play();
            Debug.Log("Loud Step");
        }
        if (player.isIdle == true || player.grounded == false)
        {
            loudRipples.Stop();
        }
        else
        {
            //Debug.Log("Why are the loud ripples playing?");
            return;
        }
        */

    }

    private void Land()
    {
        //currentRipples = loudRipples;
       loudRipples.Play();
    }

    private void RippleStop()
    {
        if (loudRipples.isPlaying)
        {
            loudRipples.Stop();
        }
        if (quietRipples.isPlaying)
        {
            quietRipples.Stop();
        }
        else
        {
            return;
        }
    }

    IEnumerator QuietRippleCoroutine()
    {
        quietRipples.Play();
        quietRipples.Stop();
        yield return new WaitForSeconds(timeBetweenRipples);

    }
    IEnumerator LoudRippleCoroutine()
    {
        loudRipples.Play();
        loudRipples.Stop();
        yield return new WaitForSeconds(timeBetweenRipples);

    }
    IEnumerator Ribbit()
    {
        loudRipples.Play();
        //play ribbit sound here
        loudRipples.Stop();
        yield return new WaitForSeconds(timeBetweenRibbits);
    }    
    /*
    private void Ribbit()
    {
        loudRipples.Play();
    
    }
    */
}
