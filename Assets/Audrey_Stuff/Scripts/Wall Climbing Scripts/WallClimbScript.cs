using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallClimbScript : MonoBehaviour
{
    public float open = 100f;
    public float range = 1f;
    public bool touchingWall = false;
    public float upwardSpeed = 3.3f;
    public Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();

        if (Input.GetKey("w") & touchingWall == true)
        {
            transform.position += Vector3.up * Time.deltaTime * upwardSpeed;
            GetComponent<Rigidbody>().isKinematic = true;
            touchingWall = false;
            GetComponent<Rigidbody>().isKinematic = false;

        }

        if (Input.GetKeyUp("w"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            touchingWall = false;
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                touchingWall = true;
            }

        }
    }

}
