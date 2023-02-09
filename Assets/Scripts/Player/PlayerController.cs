using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [Header("Movement")]
    [SerializeField] public float walkSpeed;//public pour avoir acces dans SoundGageParticles
    [SerializeField] public float sprintSpeed;//public pour avoir acces dans SoundGageParticles
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] public float climbSpeed;//public pour avoir acces dans SoundGageParticles
    [SerializeField] private Transform orientation;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    
    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Sound Gage")]
    [SerializeField] GameObject soundGageDisplay;

    [Header("References")]
    public PlayerClimbing climbingScript;
    private Animator anim;
    

    private float horizontalInput;
    private float verticalInput;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    [HideInInspector] public float moveSpeed; //public pour avoir acces dans SoundGageParticles
    private Vector3 moveDirection;

    private bool readyToJump;

    public bool grounded;

    public bool isIdle;

    public MovementState state;

    private Rigidbody rb;
    public bool climbing;

    


    public enum MovementState
    {
        Walking,
        Sprinting,
        Climbing,
        Air,
        Idle
    }

    

    protected override void Awake()
    {
        base.Awake();
        // Assigne et g�le la rotation du rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        isIdle = true;
        anim = GetComponent<Animator>();
        
       
    }

    private void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        Input();
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
        
        //Cammouflage
        
        
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Input()
    {
        horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

        //Saute lorsque le joueur est pr�t et au sol
        if (UnityEngine.Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
            anim.SetTrigger("Jump");
        }

        if (UnityEngine.Input.anyKey)
        {
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }
    }


    protected void StateHandler()
    {
        //Mode - idle
        if (isIdle == true)
        {
            state = MovementState.Idle;
            anim.SetFloat("SpeedAnimations", 0, 0.1f, Time.deltaTime);
        }

        //Mode - climbing
        if (climbing)
        {
            state = MovementState.Climbing;
            moveSpeed = climbSpeed;
            anim.SetTrigger("Climb");
        }

        //Mode - sprinting
        if (grounded && UnityEngine.Input.GetKey(sprintKey))
        {
            state = MovementState.Sprinting;
            moveSpeed = sprintSpeed;
            anim.SetFloat("SpeedAnimations", 1, 0.1f, Time.deltaTime);
        }

        //Mode - walking
        else if (grounded)
        {
            state = MovementState.Walking;
            moveSpeed = walkSpeed;
        }

        //Mode - air
        else
        {
            state = MovementState.Air;
        }
    }

    private void MovePlayer()
    {
        if (climbingScript.exitingWall) return;
        
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
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        if(grounded && verticalInput != 0 || horizontalInput != 0)
        {
            anim.SetFloat("SpeedAnimations", 0.5f, 0.1f, Time.deltaTime);
        }


        // Dans les airs
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
           
        }


        

        // Pas de gravit� sur les pentes
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //Limite la v�locit� sur les pentes
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        //Limite la v�locit� sur le sol ou dans les airs
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                //Calcule la nouvelle v�locit� si le moveSpeed d�passe celle pr��tablie
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                //Applique la nouvelle v�locit�
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        //R�initialise la v�locit� en y (le joueur saute toujours � la m�me hauteur)
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
