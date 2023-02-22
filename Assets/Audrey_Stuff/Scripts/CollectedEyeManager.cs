using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedEyeManager : MonoBehaviour
{
    public GameObject[] eye;
    public int eyeID;


    void Start()
    {
        for (int i = 0; i < eye.Length; i++)
        {
            eye[i].gameObject.SetActive(false);
        }

    }

    public void EnableEye()
    {
        eye[eyeID].gameObject.SetActive(true);
    }
    public void DisableEye()
    {
        eye[eyeID].gameObject.SetActive(false);
    }
}
