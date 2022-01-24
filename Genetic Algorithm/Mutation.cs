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
        // fill output candidate with zeros
        // double its size to we can displace to any position
        List<int> outputList = new List<int>();
        for (int i = 0; i < inputList.Count * 2; i++)
        {
            outputList.Add(0);
        }
        // selection length to move
        int sectionLength = Random.Range(2, inputList.Count - 1);
        // start and end of selection section
        int a = Random.Range(0, inputList.Count - sectionLength);
        int b = a + sectionLength;
        // choose a position to move it to
        int newPos = Random.Range(0, inputList.Count);
        while (newPos >= a && newPos <= b)
        {
            newPos = Random.Range(0, inputList.Count);
        }
        // move our selection
        for (int i = a; i <= b; i++ )
        {
            outputList[newPos] = inputList[i];
            newPos += 1;
        }
        // iterate first list filliing in blanks (0s)
        for (int i = 0; i < inputList.Count; i++)
        {
            if (outputList[i] == 0)
            {
                for (int j = 0; j < inputList.Count; j++)
                {
                    if (!outputList.Contains(inputList[j]))
                    {
                        outputList[i] = inputList[j];
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
        // create an output candidate filled with blanks
        List<int> outputList = new List<int>();
        for (int i = 0; i < inputList.Count; i++)
        {
            outputList.Add(0);
        }
        // choose a random value position
        int v = Random.Range(0, inputList.Count);
        // choose a random position to move it to
        int np = Random.Range(0, inputList.Count);
        while (v == np)
        {
            np = Random.Range(0, inputList.Count);
        }
        // add the value to new position
        outputList[np] = inputList[v];
        // iterate parent filling in blanks
        for (int i = 0; i < inputList.Count; i++)
        {
            if (outputList[i] == 0)
            {
                for (int j = 0; j < inputList.Count; j++)
                {
                    if (!outputList.Contains(inputList[j]))
                    {
                        outputList[i] = inputList[j];
                        break;
                    }
                }
            }
        }
        return outputList;
    }


    public List<int> IVM(List<int> inputList)
    // inversion mutation
    {
        List<int> outputList = new List<int>(inputList);
        // selection length
        int sectionLength = Random.Range(2, inputList.Count - 1);
        // start and end of selection
        int a = Random.Range(0, inputList.Count - sectionLength);
        int b = a + sectionLength;
        // counter moving fwd
        int i = a;
        // loop moving bkwd
        for (int j = b; j >= a; j--)
        {
            outputList[j] = inputList[i];
            i += 1;
        }        
        return outputList;
    }


    public List<int> DIVM(List<int> inputList)
    // displaced inversion mutation
    // TODO: improve this (DM too)
    {
        List<int> outputList = new List<int>();
        for (int i = 0; i < inputList.Count * 2; i++)
        {
            outputList.Add(0);
        }
        int sectionLength = Random.Range(2, inputList.Count - 1);
        int a = Random.Range(0, inputList.Count - sectionLength);
        int b = a + sectionLength;
        int newPos = Random.Range(0, inputList.Count);
        while (newPos >= a && newPos <= b)
        {
            newPos = Random.Range(0, inputList.Count);
        }
        for (int i = b; i >= a; i--)
        {
            outputList[newPos] = inputList[i];
            newPos += 1;
        }
        for (int i = 0; i < inputList.Count; i++)
        {
            if (outputList[i] == 0)
            {
                for (int j = 0; j < inputList.Count; j++)
                {
                    if (!outputList.Contains(inputList[j]))
                    {
                        outputList[i] = inputList[j];
                        break;
                    }
                }
            }
        }
        outputList.RemoveAll(i => i == 0);
        return outputList;
    }
}
