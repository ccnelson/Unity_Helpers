// C NELSON 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutation
{
    DataFunctions df;

    public Mutation()
    {
        df = new DataFunctions();
    }


    public List<int> EM(List<int> inputList)
    // exchange mutation
    {
        List<int> outputList = new List<int>();
        outputList = df.CloneIntList(inputList);
        df.TrimListFirstAndLast(outputList);
        int a = Random.Range(0, outputList.Count);
        int b = Random.Range(0, outputList.Count);
        while (a == b)
        {
            b = Random.Range(0, outputList.Count);
        }
        int temp = outputList[a];
        outputList[a] = outputList[b];
        outputList[b] = temp;
        df.InsertZeroesAtFirstAndLast(outputList);
        return outputList;
    }


    public List<int> SM(List<int> inputList)
    // scramble mutation
    {
        List<int> outputList = new List<int>();
        outputList = df.CloneIntList(inputList);
        df.TrimListFirstAndLast(outputList);
        int a = Random.Range(0, outputList.Count -1);
        int b = Random.Range(a +1, outputList.Count);
        List<int> scrambleSectionA = new List<int>();
        List<int> scrambleSectionB = new List<int>();
        for (int i = a; i <= b; i++)
        {
            scrambleSectionA.Add(outputList[i]);
            scrambleSectionB.Add(outputList[i]);
        }
        while (df.CheckListsMatch(scrambleSectionA, scrambleSectionB))
        {
            scrambleSectionB = df.ShuffleIntList(scrambleSectionA);
        }
        Queue<int> scrambleQueue = new Queue<int>(scrambleSectionB);
        for (int i = a; i <= b; i++)
        {
            outputList[i] = scrambleQueue.Dequeue();
        }
        df.InsertZeroesAtFirstAndLast(outputList);
        return outputList;
    }
}
