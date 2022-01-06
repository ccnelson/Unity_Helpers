// C NELSON 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataFunctions : MonoBehaviour
{
    public int GetRandomInt(int max) { return Random.Range(0, max); }

    public float GetRandomFloat(float min, float max) { return Random.Range(min, max); }


    public List<int> CloneIntList(List<int> inputList)
    {
        List<int> outputList = new List<int>();
        foreach (int i in inputList)
        {
            outputList.Add(i);
        }
        return outputList;
    }


    public string FormatIntList(List<int> inputList)
    {
        string outputString = "";
        foreach (int i in inputList)
        {
            outputString += i.ToString() + ", ";
        }
        return outputString;
    }


    public List<int> ShuffleIntList(List<int> inputList)
    {
        List<int> outputList = new List<int>();
        outputList = CloneIntList(inputList);
        int temp;
        for (int i = 0; i < inputList.Count; i++)
        {
            int rnd = GetRandomInt(inputList.Count);
            temp = outputList[i];
            outputList[i] = outputList[rnd];
            outputList[rnd] = temp;
        }
        return outputList;
    }


    public List<Vector3> GetDataPoints(int numberOfPoints, bool isCircular, float minVal, float maxVal)
    {
        List<Vector3> listOfPoints = new List<Vector3>();

        if (isCircular)
        {
            float theta = 0f;
            float radius = 4f;
            for (var i = 0; i < numberOfPoints; i++)
            {
                float Xpos = radius * Mathf.Cos(theta * Mathf.PI / 180f);
                float Ypos = radius * Mathf.Sin(theta * Mathf.PI / 180f);
                theta += 360f / (float)numberOfPoints;
                listOfPoints.Add(new Vector3(Xpos, Ypos, 0));
            }
        }
        else
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                listOfPoints.Add(new Vector3(GetRandomFloat(minVal, maxVal), GetRandomFloat(minVal, maxVal), 0));
            }
        }
        return listOfPoints;
    }


    public float MeasureDistance(List<Vector3> locations, List<int> sequence)
    {
        float result = 0f;

        for (int i = 0; i < sequence.Count -1; i++)
        {
            result += Vector3.Distance(locations[sequence[i]], locations[sequence[i + 1]]);
        }

        return result;
    }


    public void TrimListFirstAndLast(List<int> inputList)
    {
        inputList.RemoveAt(0);
        inputList.RemoveAt(inputList.Count - 1);
    }


    public void InsertZeroesAtFirstAndLast(List<int> inputList)
    {
        inputList.Insert(0, 0);
        inputList.Add(0);
    }


    public List<int> Crossover(List<int> _parentA, List<int> _parentB)
    {
        // clone and trim lists
        List<int> parentA = CloneIntList(_parentA);
        List<int> parentB = CloneIntList(_parentB);
        TrimListFirstAndLast(parentA);
        TrimListFirstAndLast(parentB);
        // set a random crossover somewhere inside sequence
        int crossoverPos = Random.Range(1, parentA.Count - 1);
        
        // populate offspring with placeholder values
        List<int> offSpring = new List<int>();
        foreach(int x in parentA)
        {
            offSpring.Add(0);
        }

        // copy parent A upto crossoverPos
        for (int i = 0; i < crossoverPos; i++)
        {
            offSpring[i] = parentA[i];
        }

        // copy parent B from crossoverPos, igoring duplicates
        for (int i = crossoverPos; i < offSpring.Count; i++)
        {
            if (!offSpring.Contains(parentB[i]))
            {
                offSpring[i] = parentB[i];
            }
        }

        // fill in gaps, preserving sequence from parent B
        for (int i = 0; i < offSpring.Count; i++)
        {
            if (offSpring[i] == 0)
            {
                foreach(int x in parentB)
                {
                    if(!offSpring.Contains(x))
                    {
                        offSpring[i] = x;
                        break;
                    }
                }
            }
        }
        // replace leading and ending zeroes
        InsertZeroesAtFirstAndLast(offSpring);

        return offSpring;
    }
}
