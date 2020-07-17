using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public int SEG_COUNT;
    public int CTRL_PT_COUNT;
    private float t = 1;
    private Vector3[] ctrlPointsPositions;
    private Vector3[] theCurve;

    // public 
    
    void Start()
    {
        theCurve = new Vector3[SEG_COUNT];
        CreatePoints();
        RenderCurve();
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
        // B(t) = (1-t)3P0 + 3(1-t)2tP1 + 3(1-t)t2P2 + t3P3 ; 0 < t < 1
        for (int i = 0; i < CTRL_PT_COUNT; i++)
        {
            for (int j = 0; j < SEG_COUNT; j++)
            {
                theCurve[j] = Bezier(j, ctrlPointsPositions[i]);
            }
        }
    }

    private Vector3 Bezier(float segIndex, Vector3 Pn){
        float tCoeff = BinomialCoeff(segIndex);
        Debug.Log("tCoeff");
        Debug.Log(tCoeff);
        Vector3 temp = new Vector3(0,0,0);
        temp = tCoeff * Pn;
        Debug.Log("temp");
        Debug.Log(temp);
        return temp;
    }

    private float BinomialCoeff(float segIndex){
        return(SEG_COUNT/(segIndex * (SEG_COUNT - segIndex)));
    }

    // private float Bernstein(float tCoeff, float segIndex){
    //     return tCoeff * ;
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
