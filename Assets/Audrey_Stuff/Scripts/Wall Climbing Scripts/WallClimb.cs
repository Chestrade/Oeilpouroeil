using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WallClimb : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    public LayerMask wallLayer;
    private PlayerController pm;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    public float climbTimer;

    private bool isClimbing;


    [Header("Spherecast")]
    public float castRange;
    public float sphereCastRadius;

    private Vector3 wallAngle;

    private GameObject climbableWall;

    Transform from;
    Transform to;
    float speed = 0.01f;
    float timeCount = 0.0f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerController>();
    }


    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward * castRange, out hit, castRange, wallLayer))
        {
           // Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
            print("Climb " + hit.collider.gameObject.name);
            isClimbing = true;
            climbableWall = hit.collider.gameObject;
            wallAngle = hit.transform.rotation.eulerAngles;
            print(wallAngle.normalized);

        }
        else
        {
            isClimbing = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);

        }


        if (isClimbing)
        {

            Climbing();
        }
    }

    void Climbing()
    {
        Physics.gravity = new Vector3(wallAngle.x, wallAngle.y, wallAngle.z);
      //  gameObject.transform.parent = climbableWall.transform;

        //gameObject.transform.rotation = Quaternion.Euler(wallAngle.x, wallAngle.y, wallAngle.z);


        // transform.rotation = Quaternion.Lerp(transform.rotation, wallAngle.rotation, timeCount * speed);
        //  timeCount = timeCount + Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, castRange);

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward * castRange, out hit, castRange, wallLayer))
        {
            Gizmos.color = Color.green;
            Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
            Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
            Gizmos.DrawSphere(hit.point, 0.1f);
            Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
        }
        else
        {
            Gizmos.color = Color.red;
            Vector3 sphereCastMidpoint = transform.position + (transform.forward * (wallLayer - sphereCastRadius));
            Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
            Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
        }
    }

}
