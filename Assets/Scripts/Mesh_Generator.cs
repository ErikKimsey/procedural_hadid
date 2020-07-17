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
    private int CORNER_VERT_TOTAL = 0;
    int vertCount = 0;


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
        // GenerateMesh();
    }

    void CalculateTotalVertices(){
        int e = ((xSize + ySize + zSize) * 4) + 3;
        int f = ((xSize+1)*(ySize+1) + (xSize+1)*(zSize+1) + (ySize+1)*(zSize+1)) * 2;
        TOTAL_VERTICES = f + e;
        Debug.Log("TOTAL_VERTICES");
        Debug.Log(TOTAL_VERTICES);
        vertices = new Vector3[TOTAL_VERTICES];
        StartCoroutine(MakeMeshData());
    }

    private IEnumerator MakeMeshData(){
        WaitForSeconds wait = new WaitForSeconds(0.0f);
        // for (int i = 0; i < TOTAL_VERTICES; i+=6)
        // {
        //     vertices[0] = new Vector3(i,i,i);
        //     vertices[i + 1] = new Vector3(i, i + 1, i);
        //     vertices[i + 2] = new Vector3(i + 1,i,i);
        //     vertices[i + 3] = new Vector3(i + 1,i,i);
        //     vertices[i + 4] = new Vector3(i, i+1 ,i+1);
        //     vertices[i + 5] = new Vector3(i+1 ,i+1 ,i);
        // }
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x <= xSize; x++) 
            {
                vertices[vertCount] = new Vector3(x, y, 0);
                vertCount++;
                yield return wait;
            }
            for (int z = 1; z <= zSize; z++)
            {
                vertices[vertCount] = new Vector3(0, y, z);
                vertCount++;
                yield return wait;
            }
            for (int x = xSize; x >= 0; x--)
            {
                vertices[vertCount] = new Vector3(x, y, zSize);
                vertCount++;
                yield return wait;
            }
            for (int z = zSize - 1 ; z > 0; z--)
            {
                vertices[vertCount] = new Vector3(xSize, y, z);
                vertCount++;
                yield return wait;
            }
            vertCount++;
        }
        for (int z = 1; z <= zSize; z++) {
            for (int x = 1; x < xSize; x++) {
                vertices[vertCount] = new Vector3(x, 0, z);
                vertCount++;
                yield return wait;
            }
        }
        for (int z = 0; z < zSize + 1; z++) {
            for (int x = 0; x <= xSize; x++) {
                vertices[vertCount] = new Vector3(x, ySize, z);
                vertCount++;
                yield return wait;
            }
        }
        CreateTriangles();
    }


    private void CreateTriangles () {
        tris = new int[vertices.Length];
        Debug.Log("tris.Length");
        Debug.Log(tris.Length);
        GenerateTris();
	}
    // 0,1,2,3,4,5,6,7,8
    // 0,1,2,2,1,3,3,2,4

    private void GenerateTris(){
        
        GenerateMeshData();
    }

    void GenerateMeshData(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = tris;
    }

    void OnDrawGizmos () {
		Gizmos.color = Color.black;
		for (int i = 0; i < TOTAL_VERTICES; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
    }
}
