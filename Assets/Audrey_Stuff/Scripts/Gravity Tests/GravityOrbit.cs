using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float gravity;

    public bool fixedDirection;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<GravityManager>().gravity = this.GetComponent<GravityOrbit>();

            print("Hit Orbit");
        }
    }

}
