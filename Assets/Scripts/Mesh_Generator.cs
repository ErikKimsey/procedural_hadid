using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Mesh_Generator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] tris;
    public int xSize, ySize, zSize;
    private int TOTAL_VERTICES;
    private int CORNER_VERT_TOTAL = 9;


    // (where "x", "y", and "z" are quantities of vertices / x,y,z dimensions of object)
    // Total qty of CORNER vertices:
    // 8, for cube object
    // Total qty of EDGE vertices:
    // 4 * (x + y + z - 3)
    // Total qty of FACE vertices:
    // 2 ((x-1)(y-1) + (x-1)(z-q) + (y-1)(z-1))

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        CalculateTotalVertices();
        MakeMeshData();
        GenerateMesh();
    }

    void CalculateTotalVertices(){
        int e = 4 * (xSize + ySize + zSize - 3);
        int f = 2 * ((xSize-1)*(ySize-1) + (xSize-1)*(zSize-1) + (ySize-1)*(zSize-1));
        TOTAL_VERTICES = CORNER_VERT_TOTAL + f + e;
    }

    void MakeMeshData(){
        vertices = new Vector3[]{
            new Vector3(0,0,0),
            new Vector3(0,2,0),
            new Vector3(4,0,0),
            new Vector3(4,0,0),
            new Vector3(0,2,0),
            new Vector3(1,4,5)
        };
        tris = new int[] {0,1,2,3,4,5};
        for (int i = 0; i < vertices.Length; i++)
        {
            tris[i] = i;
            
        }
    }

    void GenerateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = tris;
    }
}
