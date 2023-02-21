using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;

public class Cammouflage : MonoBehaviour
{
    public Material[] material;
    [SerializeField] private bool cammoPossible;
    [SerializeField] private GameObject frogChild;
    [SerializeField] private VisionTarget vtScript;

    private Renderer rend;
    private PlayerController player;

    [SerializeField] private MeshCollider playerColl;
    private CapsuleCollider enemyColl;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = frogChild.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        cammoPossible = false;
        
        enemyColl = GameObject.Find("Enemy/Mesh").GetComponent<CapsuleCollider>();

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
            Physics.IgnoreCollision(playerColl, enemyColl, true);
            rend.sharedMaterial = material[1];
            vtScript.visible = false;
        }
        else
        {
            Physics.IgnoreCollision(playerColl, enemyColl, false);
            rend.sharedMaterial = material[0];
            vtScript.visible = true;
        }
       
    }

    
}
