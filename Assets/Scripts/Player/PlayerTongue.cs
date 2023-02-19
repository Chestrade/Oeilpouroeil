using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongue : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private PlayerController pc;

    public Transform cam;
    public Transform tongueTip;
    public LayerMask whatIsTongueable;
    public LineRenderer lr;

    [Header("Tongue")]

    [SerializeField] private Vector3 tonguePoint;
    [SerializeField] private RaycastHit hit;

    public float maxTongueDistance;
    public float tongueDelayTime;

    [Header("Cooldown")]

    [SerializeField] private float tongueCooldownTimer;

    public float tongueCooldown;

    [Header("Input")]

    [SerializeField] bool isTonguing;
    public GameObject eyeGem;

    public KeyCode tongueKey = KeyCode.Mouse1;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(tongueKey)) StartTongue();

        if (tongueCooldownTimer > 0) tongueCooldownTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (isTonguing) lr.SetPosition(0, tongueTip.position);
    }

    private void StartTongue()
    {
        if (tongueCooldownTimer > 0) return;

        isTonguing = true;

        

        //Si la langue touche
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxTongueDistance, whatIsTongueable))
        {
            tonguePoint = hit.point;

            Invoke(nameof(ExecuteTongue), tongueDelayTime);
        }
        //Si la langue touche pas
        else
        {
            tonguePoint = cam.position + cam.forward * maxTongueDistance;

            Invoke(nameof(StopTongue), tongueDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, tonguePoint);
    }

    private void ExecuteTongue()
    {
        isTonguing = false;

        tongueCooldownTimer = tongueCooldown;

        if (hit.collider.gameObject == eyeGem)
        {
            eyeGem.transform.position = cam.position;
        }

        lr.enabled = false;
    }

    private void StopTongue()
    {
        isTonguing = false;

        tongueCooldownTimer = tongueCooldown;

        lr.enabled = false;
    }
}

/*{
    [Header("References")]

    [SerializeField] private PlayerController pc;

    public Transform cam;
    public Transform tongueTip;
    public LayerMask whatIsTongueable;
    public LineRenderer lr;

    [Header("Tongue")]

    [SerializeField] private Vector3 tonguePoint;

    public float maxTongueDistance;
    public float tongueDelayTime;

    [Header("Cooldown")]

    [SerializeField] private float tongueCooldownTimer;

    public float tongueCooldown;

    [Header("Input")]

    [SerializeField] bool isTonguing;

    public KeyCode tongueKey = KeyCode.Mouse1;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(tongueKey)) StartTongue();

        if (tongueCooldownTimer > 0) tongueCooldownTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (isTonguing) lr.SetPosition(0, tongueTip.position);
    }

    private void StartTongue()
    {
        if (tongueCooldownTimer > 0) return;

        isTonguing = true;

        RaycastHit hit;

        //Si la langue touche
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxTongueDistance, whatIsTongueable))
        {
            tonguePoint = hit.point;

            Invoke(nameof(ExecuteTongue), tongueDelayTime);
        }
        //Si la langue ne touche pas
        else
        {
            tonguePoint = cam.position + cam.forward * maxTongueDistance;

            Invoke(nameof(StopTongue), tongueDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, tonguePoint);
    }

    private void ExecuteTongue()
    {
        //Une fois que la langue a touché quelque chose, on veut qu'elle revienne à la position de la caméra. On peut utiliser la fonction MovePosition() du transform pour déplacer la position de la langue vers la position de la caméra.

        tongueTip.position = cam.position;

        StopTongue();
    }

    private void StopTongue()
    {
        isTonguing = false;

        tongueCooldownTimer = tongueCooldown;

        lr.enabled = false;
    }
}*/
