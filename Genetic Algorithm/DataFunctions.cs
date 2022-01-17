// C NELSON 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataFunctions
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


    public string GetFactorial(int n)
    {
        System.Numerics.BigInteger result = 1;
        for (int i = n; i > 0; i--)
        {
            result *= i;
        }
        return result.ToString("#,##0");
    }

    public List<int> Mutate(List<int> inputList)
    {
        List<int> outputList = new List<int>();
        outputList = CloneIntList(inputList);
        TrimListFirstAndLast(outputList);

        int a = Random.Range(0, outputList.Count);
        int b = Random.Range(0, outputList.Count);
        while (a == b)
        {
            b = Random.Range(0, outputList.Count);
        }

        int temp = outputList[a];
        outputList[a] = outputList[b];
        outputList[b] = temp;

        InsertZeroesAtFirstAndLast(outputList);

        return outputList;
    }
}
