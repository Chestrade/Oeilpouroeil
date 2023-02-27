using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public GravityOrbit gravity;
    private Rigidbody rb;
    public float rotationSpeed = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gravity)
        {
            Vector3 gravityUp = Vector3.zero;

            if (gravity.fixedDirection)
            {
                gravityUp = gravity.transform.up;
            }
            else
            {
                gravityUp = (transform.position - gravity.transform.position).normalized;
            }

            Vector3 localUp = transform.up;

            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            rb.GetComponent<Rigidbody>().rotation = targetRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

            rb.AddForce((-gravityUp * gravity.gravity) * rb.mass);
        }
        else
        {
            Vector3 gravityUp = Vector3.zero;

        }
    }
}








