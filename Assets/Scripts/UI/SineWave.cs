using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=6C1NPy321Nk

public class SineWave : MonoBehaviour
{
    [SerializeField] private LineRenderer sineRenderer;
    private Material sineMaterial;
    [SerializeField] private int points;
    [SerializeField] private float amplitude;
    private float minAmp;
    private float maxAmp;
    [SerializeField] private float frequency; //influencé par la vitesse à laquelle le joueur se promène
    [SerializeField] private Vector2 xLimits;
    [SerializeField] private float movementSpeed;

    void Start()
    {
        sineRenderer = GetComponent<LineRenderer>();
        sineMaterial = sineRenderer.material;

        minAmp = 0.00f;

        maxAmp = 1.00f;
    }

    void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        sineRenderer.positionCount = points;
        for(int currentPoint = 0; currentPoint<points;currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude*Mathf.Sin((Tau*frequency*x)+Time.timeSinceLevelLoad*movementSpeed);
            sineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    private void Update()
    {
        Draw();



        if (amplitude >= maxAmp)
        {
            amplitude = maxAmp;
        }

        if (amplitude <= minAmp)
        {
            amplitude = minAmp;
        }
        if (amplitude > minAmp && amplitude <= 0.05f)
        {
            sineMaterial.SetColor("_EmissionColor", Color.green);
        }
        if(amplitude >0.05f &&  amplitude <= maxAmp)
        {
            sineMaterial.SetColor("_EmissionColor", Color.red);
        }
        

    }
}
