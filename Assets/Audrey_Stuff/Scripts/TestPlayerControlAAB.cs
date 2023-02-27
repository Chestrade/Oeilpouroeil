using IndieMarc.EnemyVision;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HELLO DO NOT USE THIS, THIS SCRIPT IS FOR TESTING ONLY AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH. Thank you for your cooperation.

public class TestPlayerControlAAB : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed = 5f;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
    }
}


/*
public float speed = 6.0F;
public float gravity = 20.0F;

private Vector3 moveDirection = Vector3.zero;
public CharacterController controller;


void Start()
{
    // Store reference to attached component
    controller = GetComponent<CharacterController>();
}

void Update()
{
    // Character is on ground (built-in functionality of Character Controller)
    if (controller.isGrounded)
    {
        // Use input up and down for direction, multiplied by speed
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
    }
    // Apply gravity manually.
    moveDirection.y -= gravity * Time.deltaTime;
    // Move Character Controller
    controller.Move(moveDirection * Time.deltaTime);

    print(Input.GetAxis("Vertical"));
}
*/

