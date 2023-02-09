using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cammouflage : SoundGageParticles
{
    public Material[] material;
    [SerializeField] private bool cammoPossible;
    [SerializeField] private GameObject frogChild;
    private Renderer rend;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = frogChild.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        cammoPossible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && cammoPossible == false)
        {
            cammoPossible = true;
        }
        else if(Input.GetKeyDown(KeyCode.C) && cammoPossible == true)
        {
            cammoPossible = false;            
        }
        if(player.isIdle && player.grounded && cammoPossible)
        {
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend.sharedMaterial = material[0];
        }
       
    }
}
