// C NELSON 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataFunctions
{
    public int GetRandomInt(int max) { return Random.Range(0, max); }

    public float GetRandomFloat(float min, float max) { return Random.Range(min, max); }


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
        List<int> outputList = new List<int>(inputList);
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


    public string GetFactorial(int n)
    {
        System.Numerics.BigInteger result = 1;
        for (int i = n; i > 0; i--)
        {
            result *= i;
        }
        return result.ToString("#,##0");
    }


    public bool CheckListsMatch(List<int> lista, List<int> listb)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i] != listb[i])
            {
                return false;
            }
        }
        return true;
    }
}
