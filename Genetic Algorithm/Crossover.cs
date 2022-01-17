// C NELSON 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossover
{
    DataFunctions df;

    public Crossover()
    {
        df = new DataFunctions();
    }

    public List<int> MPX(List<int> _parentA, List<int> _parentB)
    // maximal preservation crossover
    {
        Debug.Log("--------");
        Debug.Log(df.FormatIntList(_parentA));
        Debug.Log(df.FormatIntList(_parentB));
        // clone and trim lists
        List<int> parentA = df.CloneIntList(_parentA);
        List<int> parentB = df.CloneIntList(_parentB);
        df.TrimListFirstAndLast(parentA);
        df.TrimListFirstAndLast(parentB);
        // set a random crossover somewhere inside sequence
        int crossoverPos = Random.Range(1, parentA.Count -1);
        Debug.Log(crossoverPos);
        Debug.Log("--------");
        // populate offspring with placeholder values
        List<int> offSpring = new List<int>();
        foreach (int x in parentA)
        {
            offSpring.Add(0);
        }

        // copy parent A upto crossoverPos
        for (int i = 0; i <= crossoverPos; i++)
        {
            offSpring[i] = parentA[i];
        }

        // copy parent B from crossoverPos, igoring duplicates
        for (int i = crossoverPos +1; i < offSpring.Count; i++)
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
                foreach (int x in parentB)
                {
                    if (!offSpring.Contains(x))
                    {
                        offSpring[i] = x;
                        break;
                    }
                }
            }
        }
        // replace leading and ending zeroes
        df.InsertZeroesAtFirstAndLast(offSpring);

        return offSpring;
    }
}
