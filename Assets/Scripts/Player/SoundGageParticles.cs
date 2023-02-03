using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGageParticles : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem quietRipples;
    [SerializeField] private ParticleSystem loudRipples;
    private ParticleSystem currentRipples;

    private PlayerController player;

    private void Start()
    {
        player = PlayerController.instance;
        currentRipples = null;
    }
    private void QuietRipple()
    {
       
        currentRipples = quietRipples;
        if(loudRipples.isPlaying)
        {
            loudRipples.Stop();
        }
        if (player.isIdle == false) 
        {
            currentRipples.Play();
            Debug.Log("Quiet Step");
        }
        else if(player.isIdle == true || player.grounded == false)
        {
            currentRipples.Stop();
        }
        
    }

    private void LoudRipple()
    {
        currentRipples = loudRipples;
        if(quietRipples.isPlaying)
        {
            quietRipples.Stop();
        }
        if (player.isIdle == false)
        {
            currentRipples.Play();
            Debug.Log("Loud Step");
        }
        else if (player.isIdle == true || player.grounded == false)
        {
            currentRipples.Stop();
        }

    }

    private void Land()
    {
        currentRipples = loudRipples;
        if (quietRipples.isPlaying)
        {
            quietRipples.Stop();
        }
        currentRipples.Play();
    }

    private void RippleStop()
    {
        currentRipples.Stop();
    }
}
