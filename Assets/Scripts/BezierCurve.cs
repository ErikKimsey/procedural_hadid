using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public int SEG_COUNT;
    public int CTRL_PT_COUNT;
    private float t;
    private Vector3[] ctrlPointsPositions;
    private Vector3[] theCurve;

    // public 
    
    void Start()
    {
        t = 1/SEG_COUNT;
        theCurve = new Vector3[SEG_COUNT];
        
        CreatePoints();
    }

    private void CreatePoints(){
        ctrlPointsPositions = new Vector3[]{
            new Vector3(0,0,0),
            new Vector3(1,2,2),
            new Vector3(1,4,4),
            new Vector3(4,2,0)
        };
    }

    private void RenderCurve(){
        // line = 
        for (int i = 0; i < SEG_COUNT; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
