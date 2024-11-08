using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealWorldTracking : MonoBehaviour
{
    public GameObject trackedObject;
    public GameObject[] anchorObjects;

    public GameObject trackerMarkerPrefab;
    private GameObject trackerMarker;
    public float proximityCutOff = .01f;
    public int reccursionCutOff = 4;

    private float[] distances;
    private Vector3[] positions;
    private int anchorAmount;

    private List<GameObject> trackers;





    //creates radius around anchor points that intersect with trackedObject
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach(GameObject anchor in anchorObjects)
        {
            Gizmos.DrawWireSphere(anchor.transform.position, Vector3.Distance(trackedObject.transform.position, anchor.transform.position));
        }
    }

    
    void Start()
    {
        List<GameObject> trackers = new List<GameObject>();
        anchorAmount = anchorObjects.Length;
        distances = new float[anchorAmount];
        positions = new Vector3[anchorAmount];

        for(int i = 0; i < anchorAmount; i++)
        {
            positions[i] = anchorObjects[i].transform.position;
            distances[i] = Vector3.Distance(trackedObject.transform.position, positions[i]);
        }

        trackerMarker = Instantiate(trackerMarkerPrefab);

        /*
        foreach(Vector3 possiblePosition in trilateratePositionRelativeToAnchors(distances, positions))
        {
            var trackerInstant = Instantiate(trackerMarker, possiblePosition, trackerMarker.transform.rotation);
            trackers.Add(trackerInstant);
        }
        */
    }

    
    void Update()
    {
        for(int i = 0; i < anchorAmount; i++)
        {
            positions[i] = anchorObjects[i].transform.position;
            distances[i] = Vector3.Distance(trackedObject.transform.position, positions[i]);
        }



        /*

        Vector3[] pos = trilateratePositionRelativeToAnchors(distances, positions);

        for(int i = 0; i < trackers.Count; i ++)
        {
            if(i<pos.Length)
            {
                trackers[i].transform.position = pos[i];
            }
        }

        */
        

        trackerMarker.transform.position = trilateratePositionRelativeToAnchors(distances, positions);
        
    }


    private Vector3 trilateratePositionRelativeToAnchors(float[] distanceToAnchors, Vector3[] positionOfAnchors)
    {

        Vector3[] updatedPosition1 = Trilaterate(positionOfAnchors[0], distanceToAnchors[0], positionOfAnchors[1], distanceToAnchors[1], positionOfAnchors[2], distanceToAnchors[2]);
        Vector3[] updatedPosition2 = Trilaterate(positionOfAnchors[1], distanceToAnchors[1], positionOfAnchors[2], distanceToAnchors[2], positionOfAnchors[3], distanceToAnchors[3]);
        Vector3[] updatedPosition3 = Trilaterate(positionOfAnchors[2], distanceToAnchors[2], positionOfAnchors[3], distanceToAnchors[3], positionOfAnchors[0], distanceToAnchors[0]);
        Vector3[] updatedPosition4 = Trilaterate(positionOfAnchors[3], distanceToAnchors[3], positionOfAnchors[0], distanceToAnchors[0], positionOfAnchors[1], distanceToAnchors[1]);

        List<Vector3> allPositions = new List<Vector3>();

        for(int i = 0; i<2; i++)
        {
            if(i<updatedPosition1.Length)
            {
                allPositions.Add(updatedPosition1[i]);
            }
            if(i<updatedPosition2.Length)
            {
                allPositions.Add(updatedPosition2[i]);
            }
            if(i<updatedPosition3.Length)
            {
                allPositions.Add(updatedPosition3[i]);
            }
            if(i<updatedPosition4.Length)
            {
                allPositions.Add(updatedPosition4[i]);
            }
        }
        
        int reccursions = 0;

        for(int i = 0; i < allPositions.Count+1; i++)
        {
            for(int k = i; k < allPositions.Count; k++)
            {
                if(Vector3.Distance(allPositions[i],allPositions[k])<proximityCutOff)
                {
                    reccursions++;
                }
            }

            if(reccursions>reccursionCutOff-1)
            {
                return allPositions[i];
            }

            reccursions = 0;
        }

        

        //Vector3 average = Average(allPositions.ToArray());

        
        

        return(allPositions[0]);
    }

    public static Vector3 Average(Vector3[] vectors)
    {
        Vector3 sum = Vector3.zero;

        for (int i = 0; i < vectors.Length; i++)
        {
            sum += vectors[i];
        }

        return sum / vectors.Length;
    }








    //code below written by gheja https://github.com/gheja/trilateration.js/blob/master/trilateration.js
    static float sqr(float a)
    {
        return a * a;
    }
    static float norm(Vector3 a)
    {
        return Mathf.Sqrt(sqr(a.x) + sqr(a.y) + sqr(a.z));
    }
    static float dot(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }
    static Vector3 vector_cross(Vector3 a, Vector3 b)
    {
        return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
    }

    private static Vector3[] Trilaterate(Vector3 p1, float r1, Vector3 p2, float r2, Vector3 p3, float r3)
    {
        Vector3 ex = (p2 - p1) / norm(p2 - p1);
        float i = dot(ex, (p3 - p1));
        Vector3 a = ((p3 - p1) - (ex * i));
        Vector3 ey = (a / norm(a));
        Vector3 ez = vector_cross(ex, ey);
        float d = norm(p2 - p1);
        float j = dot(ey, p3 - p1);

        float x = (sqr(r1) - sqr(r2) + sqr(d)) / (2 * d);
        float y = (sqr(r1) - sqr(r3) + sqr(i) + sqr(j)) / (2 * j) - (i / j) * x;

        float b = sqr(r1) - sqr(x) - sqr(y);

        // floating point math flaw in IEEE 754 standard
        // see https://github.com/gheja/trilateration.js/issues/2
        if (Mathf.Abs(b) < 0.0000000001)
        {
            b = 0;
        }

        float z = Mathf.Sqrt(b);

        // no solution found
        if (float.IsNaN(z))
        {
            return new Vector3[] { Vector3.zero };
        }

        Vector3 aa = p1 + ((ex * x) + (ey * y));
        Vector3 p4a = (aa + (ez * z));
        Vector3 p4b = (aa - (ez * z));

        return new Vector3[] { p4a, p4b };
    }
}
