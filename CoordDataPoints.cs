//C NELSON 2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a list of vectors representing cartesian coordinates
// in a 10x10 grid.
// Boolean flag circularPoints creates either: 
// (true) points in a circular pattern, or
// (false) random points


public class CoordDataPoints : MonoBehaviour
{
    public int numOfPoints = 21;
    public List<Vector3> listOfPoints;
    public bool circularPoints = true;

    float randomPoint { get { return Random.Range(-5f, 5f); } }

    private void Awake()
    {
        if (circularPoints)
        {
            float theta = 0f;
            float radius = 4f;

            for (var i = 0; i < numOfPoints; i++)
            {
                float Xpos = radius * Mathf.Cos(theta * Mathf.PI / 180f);
                float Ypos = radius * Mathf.Sin(theta * Mathf.PI / 180f);
                theta += 360f / (float)numOfPoints;

                listOfPoints.Add(new Vector3(Xpos, Ypos, 0));
            }
        }
        else
        {
            for (int i = 0; i < numOfPoints; i++)
            {
                listOfPoints.Add(new Vector3(randomPoint, randomPoint, 0));
            }
        }
    } 
}
