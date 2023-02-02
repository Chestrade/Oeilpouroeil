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

        if (player.isIdle == false) 
        {
            currentRipples.Play();
        }
        else if(player.isIdle == true)
        {
            currentRipples.Stop();
        }
        
    }

    private void LoudRipple()
    {
        currentRipples = loudRipples;
        if (player.isIdle == false)
        {
            currentRipples.Play();
        }
        else if (player.isIdle == true)
        {
            currentRipples.Stop();
        }
    }

    private void RippleStop()
    {
        currentRipples.Stop();
    }
}
