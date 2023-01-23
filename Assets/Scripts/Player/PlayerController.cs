using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handeling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    [Header("Sound Gage")]
    SoundGage soundGage;
    [SerializeField] GameObject soundGageDisplay;

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        // Assigne et gèle la rotation du rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        soundGage = soundGageDisplay.GetComponent<SoundGage>();
        soundGage.amplitude = 0.010f;

    }

    private void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        SpeedControl();

        StateHandler();

        //Handle drag;
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
            
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Saute lorsque le joueur est prêt et au sol
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {
        //Mode - sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //Mode - walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        //Mode - air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Calcule la direction du mouvement (le joueur marche toujours dans la direction qu'il regarde)
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Sur les pentes
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            // Permet de rester sur la pente (le joueur bondissait en montant une pente)
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
                
        }

        // Sur le sol
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        if(grounded && verticalInput > 0 || horizontalInput>0)
        {
            soundGage.amplitude = 0.060f;
            soundGage.frequency = 10f;
        }
        if (grounded && verticalInput < 0 || horizontalInput < 0)
        {
            soundGage.amplitude = 0.060f;
            soundGage.frequency = 10f;
        }

        // Dans les airs
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            soundGage.amplitude = 0.001f;
            soundGage.frequency = 1f;
        }
        else
        {
            soundGage.amplitude = 0.001f;
            soundGage.frequency = 1f;
        }

        // Pas de gravité sur les pentes
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //Limite la vélocité sur les pentes
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        //Limite la vélocité sur le sol ou dans les airs
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                //Calcule la nouvelle vélocité si le moveSpeed dépasse celle préétablie
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                //Applique la nouvelle vélocité
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        //Réinitialise la vélocité en y (le joueur saute toujours à la même hauteur)
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Applique la force qu'une seule fois
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
