using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event levelStart;
    public AK.Wwise.Event levelEnd;
    void Start()
    {
        levelStart.Post(gameObject);
    }
}
