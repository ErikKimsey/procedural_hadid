using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezierCurve : MonoBehaviour
{

    // public 
    
        public List<Vector3> pathPoints;
		private int segments;
		public int pointCount;
        public List<Vector3> controlPoints;
        public GameObject gloCube;
        private GameObject[] gameObjects;
		public GameObject hadidParent;

        void Start()
        {
            Debug.Log(pointCount);
			pathPoints = new List<Vector3>();
			pointCount = 100;
            CreateCtrlPoints();
            CreateCurve(controlPoints);
            gameObjects = new GameObject[pointCount];
        }
		
		public void CreateCtrlPoints()
		{
            controlPoints.Add(new Vector3(-2,0,-5));
            controlPoints.Add(new Vector3(0,5,30));
            controlPoints.Add(new Vector3(3,0,-30));
            controlPoints.Add(new Vector3(6,4,0));
		}

		public void DeletePath()
		{
			pathPoints.Clear();
		}
		
		Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{	
			float tt = t * t;
			float ttt = t * tt;
			float u = 1.0f - t;
			float uu = u * u;
			float uuu = u * uu;
			
			Vector3 B = new Vector3();
			B = uuu * p0;
			B += 3.0f * uu * t * p1;
			B += 3.0f * u * tt * p2;
			B += ttt * p3;
			return B;
		}
		
		public void CreateCurve(List<Vector3> controlPoints)
		{
			segments = controlPoints.Count / 3;

			for (int s = 0; s < controlPoints.Count -3; s+=3) 
			{
				Vector3 p0 = controlPoints[s];
				Vector3 p1 = controlPoints[s+1];
				Vector3 p2 = controlPoints[s+2]; 
				Vector3 p3 = controlPoints[s+3];

				if(s == 0)
				{
					pathPoints.Add(BezierPathCalculation(p0, p1, p2, p3, 0.0f));
				}    

				for (int p = 0; p < (pointCount/segments); p++) 
				{
					float t = (1.0f / (pointCount/segments)) * p;
					Vector3 point = new Vector3 ();
					point = BezierPathCalculation (p0, p1, p2, p3, t);
					pathPoints.Add (point);
                    InstantiateObjs(point, p);
				}
			}
		}

        public void InstantiateObjs(Vector3 point, float t){
			float rotateX = (t * 10f);
			float rotateY = t * point.x + 133f;
			float rotateZ = t * point.x + 133f;
			Debug.Log(rotateZ);
			Vector3 r = new Vector3(rotateX, rotateY, rotateZ);
            GameObject clone = Instantiate(gloCube, point, Quaternion.identity, this.transform);
			clone.transform.Rotate(r, Space.World);
        }
}
