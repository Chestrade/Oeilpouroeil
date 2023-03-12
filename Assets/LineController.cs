using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    public Transform[] points;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        SetUpLine(points);
    }

    void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }


    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
