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

    public List<int> MPX(List<int> parentA, List<int> parentB)
    // maximal preservation crossover
    {
        // set a random crossover somewhere inside sequence
        int crossoverPos = Random.Range(1, parentA.Count -1);
        // populate offspring with placeholder values
        List<int> offSpring = new List<int>();
        for (int i = 0; i < parentA.Count; i++)
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
                for (int j = 0; j < parentB.Count; j++)
                {
                    if (!offSpring.Contains(parentB[j]))
                    {
                        offSpring[i] = parentB[j];
                        break;
                    }
                }
            }
        }
        return offSpring;
    }

    public List<int> PMX(List<int> parentA, List<int> parentB)
    // partially mapped crossover
    {
        // choose 2 random points (starting a at zero and ending b at count would perhaps give more diversity?)
        int a = Random.Range(1, parentA.Count - 2);
        int b = Random.Range(a + 1, parentA.Count -1);
        List<int> offspring = new List<int>();
        // initialise with zeros
        for (int i = 0; i < parentA.Count; i++)
        {
            offspring.Add(0);
        }
        // copy chosen positions from parentB
        for (int i = a; i <= b; i++)
        {
            offspring[i] = parentB[i];
        }
        // iterate using parentA to fill in blanks
        for (int i = 0; i < offspring.Count; i++)
        {
            if (offspring[i] == 0)
            {
                for (int j = 0; j < parentA.Count; j++)
                {
                    if (!offspring.Contains(parentA[j]))
                    {
                        offspring[i] = parentA[j];
                        break;
                    }
                }
            }
        }
        return offspring;
    }


    public List<int> OBX(List<int> parentA, List<int> parentB)
    // order based crossover
    {
        // start with parent B
        List<int> offspring = new List<int>(parentB);
        // choose a random number of positions, at least 2 (its a combination), but less than all of them
        int noOfPositions = Random.Range(2, parentA.Count -1);
        List<int> positionsChosen = new List<int>();
        // generate this number of valid (unique) positions from within the gene sequence
        for (int i = 0; i < noOfPositions; i++)
        {
            int r = Random.Range(0, parentA.Count);
            while(positionsChosen.Contains(r))
            {
                r = Random.Range(0, parentA.Count);
            }
            positionsChosen.Add(r);
        }
        // and put them in order
        positionsChosen.Sort();
        // get the values residing at these positions in parent A, and put in a queue for later
        List<int> valuesChosen = new List<int>();
        Queue<int> valuesChosenQ = new Queue<int>();
        for (int i = 0; i < positionsChosen.Count; i++)
        {
            valuesChosen.Add(parentA[positionsChosen[i]]);
            valuesChosenQ.Enqueue(parentA[positionsChosen[i]]);
        }
        // iterate offspring, imposing the order of the values chosen from parent A
        for (int i = 0; i < offspring.Count; i++)
        {
            if (valuesChosen.Contains(offspring[i]))
            {
                offspring[i] = valuesChosenQ.Dequeue();
            }
        }
        return offspring;
    }
}
