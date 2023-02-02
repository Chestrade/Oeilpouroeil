using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Variables
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float minValue = -30f;
    [SerializeField] private float maxValue = 30f;
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;

    //References
    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
        UpView();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yRotation += mouseX;
        parent.Rotate(Vector3.up, mouseX);

    }

    private void UpView () 
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minValue, maxValue);
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
    }
}
