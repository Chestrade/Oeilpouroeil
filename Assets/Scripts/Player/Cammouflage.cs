using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;

public class Cammouflage : MonoBehaviour
{

    [Header("Materials and Refs")]
    public Material[] material;
    [SerializeField] private bool cammoPossible;
    [SerializeField] private GameObject frogChild;
    [SerializeField] private VisionTarget vtScript;
    [SerializeField] private MeshCollider playerColl;
    [SerializeField] protected ParticleSystem enterCammoParticles;
    [SerializeField] protected ParticleSystem exitCammoParticles;

    [Header("Wwise Events")]
    public AK.Wwise.Event cammoEnter;
    public AK.Wwise.Event cammoExit;

    public bool isCammouflaged;

    private Renderer rend;
    private PlayerController player;
    private CapsuleCollider enemyColl;
    private bool wwiseEventPlayed;

    //particle system variables
    
    



    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = frogChild.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        cammoPossible = false;
        wwiseEventPlayed = false;
        isCammouflaged = false;
        
        
        
        enemyColl = GameObject.Find("Enemy/Mesh").GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        ToggleCammo();
        UseCammo();
    }

    public void ToggleCammo()
    {
        //changer le code ici pour que cammouflage s'active avec la statue
        if (Input.GetKeyDown(KeyCode.C) && cammoPossible == false)
        {
            cammoPossible = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cammoPossible == true)
        {
            cammoPossible = false;
        }
    }

    void UseCammo()
    {
        if (player.isIdle && player.grounded && cammoPossible)
        {
            isCammouflaged = true;
            Physics.IgnoreCollision(playerColl, enemyColl, true);
            rend.sharedMaterial = material[1];
            vtScript.visible = false;
            
           


            if (wwiseEventPlayed == false)
            {
                cammoEnter.Post(gameObject);
                enterCammoParticles.Play();
                //Debug.Log("The frog is cammouflaged");
                wwiseEventPlayed = true;
            }

        }
        else
        {
            isCammouflaged = false;
            Physics.IgnoreCollision(playerColl, enemyColl, false);
            rend.sharedMaterial = material[0];
            vtScript.visible = true;

            if (wwiseEventPlayed == true)
            {
                cammoExit.Post(gameObject);
                exitCammoParticles.Play();
                //Debug.Log("The frog is not cammouflaged");
                wwiseEventPlayed = false;
            }

        

        }
    }
    
}
