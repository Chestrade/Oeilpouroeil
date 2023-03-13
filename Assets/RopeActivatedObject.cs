using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeActivatedObject : MonoBehaviour
{
    public RopePull myRope;
    public bool startAction;



    void Update()
    {
        if(myRope.triggerAction == true)
        {
            startAction = true;
           // myRope.triggerAction = false;
        }
    }
}
