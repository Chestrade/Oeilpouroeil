using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGageParticles : MonoBehaviour
{
    private Transform player;
    [SerializeField] private ParticleSystem quietRipples;
    [SerializeField] private ParticleSystem loudRipples;
    private int stepCountDebug;

    
    void Start()
    {
        player = PlayerController.instance.transform;
        stepCountDebug = 0;
    }

    
    void Update()
    {

        
    }

    private void WalkStep()
    {
        


        stepCountDebug = stepCountDebug + 1;
        Debug.Log("Step " + stepCountDebug);
    }
}
