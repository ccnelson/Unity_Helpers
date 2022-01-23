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
        List<int> outputList = new List<int>(inputList);
        int a = Random.Range(0, outputList.Count);
        int b = Random.Range(0, outputList.Count);
        while (a == b)
        {
            b = Random.Range(0, outputList.Count);
        }
        int temp = outputList[a];
        outputList[a] = outputList[b];
        outputList[b] = temp;
        return outputList;
    }


    public List<int> SM(List<int> inputList)
    // scramble mutation
    {
        List<int> outputList = new List<int>(inputList);
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
        return outputList;
    }


    public List<int> DM(List<int> inputList)
    // displacement mutation
    {
        List<int> listIn = new List<int>(inputList);
        // fill output candidate with zeros
        // double its size to we can displace to any position
        List<int> outputList = new List<int>();
        for (int i = 0; i < listIn.Count * 2; i++)
        {
            outputList.Add(0);
        }
        // selection length to move
        int sectionLength = Random.Range(2, listIn.Count - 1);
        // start and end of selection section
        int a = Random.Range(0, listIn.Count - sectionLength);
        int b = a + sectionLength;
        // choose a position to move it to
        int newPos = Random.Range(0, listIn.Count);
        while (newPos >= a && newPos <= b)
        {
            newPos = Random.Range(0, listIn.Count);
        }
        // move our selection
        for (int i = a; i <= b; i++ )
        {
            outputList[newPos] = listIn[i];
            newPos += 1;
        }
        // iterate first list filliing in blanks (0s)
        for (int i = 0; i < listIn.Count; i++)
        {
            if (outputList[i] == 0)
            {
                for (int j = 0; j < listIn.Count; j++)
                {
                    if (!outputList.Contains(listIn[j]))
                    {
                        outputList[i] = listIn[j];
                        break;
                    }
                }
            }
        }
        // strip all zeroes placeholders
        outputList.RemoveAll(i => i == 0);
        return outputList;
    }


    public List<int> IM(List<int> inputList)
    // insertion mutation
    {
        List<int> listIn = new List<int>(inputList);
        // create an output candidate filled with blanks
        List<int> outputList = new List<int>();
        for (int i = 0; i < listIn.Count; i++)
        {
            outputList.Add(0);
        }
        // choose a random value position
        int v = Random.Range(0, listIn.Count);
        // choose a random position to move it to
        int np = Random.Range(0, listIn.Count);
        while (v == np)
        {
            np = Random.Range(0, listIn.Count);
        }
        // add the value to new position
        outputList[np] = listIn[v];
        // iterate parent filling in blanks
        for (int i = 0; i < listIn.Count; i++)
        {
            if (outputList[i] == 0)
            {
                for (int j = 0; j < listIn.Count; j++)
                {
                    if (!outputList.Contains(listIn[j]))
                    {
                        outputList[i] = listIn[j];
                        break;
                    }
                }
            }
        }
        return outputList;
    }
}
