using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // La cam�ra bouge avec le joueur
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }

}
