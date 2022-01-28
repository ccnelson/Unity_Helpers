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


    public List<int> PBX(List<int> parentA, List<int> parentB)
    // position based crossover
    {
        List<int> offspring = new List<int>(parentA);
        int numberOfPos = Random.Range(2, offspring.Count);
        List<int> posList = new List<int>();
        // create list of positons to clear
        for (int i = 0; i < numberOfPos; i++)
        {
            int p = Random.Range(0, offspring.Count);
            while (posList.Contains(p))
            {
                p = Random.Range(0, offspring.Count);
            }
            posList.Add(p);
        }
        // clear chosen positions
        for (int i = 0; i < offspring.Count; i++)
        {
            if (posList.Contains(i))
            {
                offspring[i] = 0;
            }
        }
        // fill in blanks maintaining parent Bs order
        for (int i = 0; i < offspring.Count; i++)
        {
            if (offspring[i] == 0)
            {
                for (int j = 0; j < parentB.Count; j++)
                {
                    if (!offspring.Contains(parentB[j]))
                    {
                        offspring[i] = parentB[j];
                        break;
                    }
                }
            }
        }
        return offspring;
    }

    public List<int> APX(List<int> parentA, List<int> parentB)
    // alternating position crossover
    {
        List<int> offspring = new List<int>();
        for (int i = 0; i < parentA.Count; i++)
        {
            if (offspring.Count != parentA.Count)
            {
                if (!offspring.Contains(parentA[i]))
                {
                    offspring.Add(parentA[i]);
                }
            }
            if (offspring.Count != parentA.Count)
            {
                if (!offspring.Contains(parentB[i]))
                {
                    offspring.Add(parentB[i]);
                }
            }
            if (offspring.Count == parentA.Count)
            {
                break;
            }
        }
        return offspring;
    }


    public List<int> MOX(List<int> parentA, List<int> parentB)
    // modified order crossover
    {
        List<int> offspring = new List<int>(parentB);
        int mid = (int)parentA.Count / 2;
        Queue<int> pAvals = new Queue<int>();
        // loop from mid point of A
        for (int i = mid; i < parentA.Count; i++)
        {
            // loop thru offspring looking for values to remove
            for (int j = 0; j < offspring.Count; j++)
            {
                if (offspring[j] == parentA[i])
                {
                    offspring[j] = 0;
                }
            }
            // queue em up
            pAvals.Enqueue(parentA[i]);
        }
        // loop through offspring replacing blanks with queued
        for (int i = 0; i < offspring.Count; i++)
        {
            if (offspring[i] == 0)
            {
                offspring[i] = pAvals.Dequeue();
            }
        }
        return offspring;
    }


    public List<int> MPMX(List<int> parentA, List<int> parentB)
    // modified partially mapped crossover
    {
        List<int> offspring = new List<int>(parentA);
        // split into 3 sections
        int a = Random.Range(1, parentA.Count - 2);
        int b = Random.Range(a+1, parentA.Count - 1);
        // remove all but the middle section of A
        for (int i = 0; i < a; i++)
        {
            offspring[i] = 0;
        }
        for (int i = b+1; i < offspring.Count; i++)
        {
            offspring[i] = 0;
        }
        // use B values able to retain oringinal position
        for (int i = 0; i < parentB.Count; i++)
        {
            if (offspring[i] == 0)
            {
                if (!offspring.Contains(parentB[i]))
                {
                    offspring[i] = parentB[i];
                }
            }
        }
        // fill in blanks randomly
        for (int i = 0; i < offspring.Count; i++)
        {
            if (offspring[i] == 0)
            {
                int rInd = Random.Range(0, parentB.Count);
                while (offspring.Contains(parentB[rInd]))
                {
                    rInd = Random.Range(0, parentB.Count);
                }
                offspring[i] = parentB[rInd];
            }
        }
        return offspring;
    }


    public List<int> CX(List<int> parentA, List<int> parentB)
    // cyclic crossover
    {
        List<int> offspring = new List<int>();
        // empty candidate
        for (int i = 0; i < parentA.Count; i++)
        {
            offspring.Add(0);
        }
        bool flip = false;
        int len = parentA.Count - 1;
        // iterate alternating parents, taking values from end & moving inwards
        for (int i = 0; i < parentA.Count / 2; i++)
        {
            if (!flip)
            {
                if (offspring[i] == 0)
                {
                    if (!offspring.Contains(parentA[i]))
                    {
                        offspring[i] = parentA[i];
                    }
                }
                if (offspring[len] == 0)
                {
                    if (!offspring.Contains(parentB[len]))
                    {
                        offspring[len] = parentB[len];
                    }
                }
            }
            else
            {
                if (offspring[i] == 0)
                {
                    if (!offspring.Contains(parentB[i]))
                    {
                        offspring[i] = parentB[i];
                    }       
                }
                if (offspring[len] == 0)
                {
                    if (!offspring.Contains(parentA[len]))
                    {
                        offspring[len] = parentA[len];
                    }                    
                }
            }
            len -= 1;
            flip = !flip;
        }
        // fill in blanks randomly
        for (int i = 0; i < offspring.Count; i++)
        {
            if (offspring[i] == 0)
            {
                int rInd = Random.Range(0, parentB.Count);
                while (offspring.Contains(parentB[rInd]))
                {
                    rInd = Random.Range(0, parentB.Count);
                }
                offspring[i] = parentB[rInd];
            }
        }
        return offspring;
    }


    public List<int> ERX(List<int> parentA, List<int> parentB)
    // edge recombination crossover
    {
        List<int> offspring = new List<int>();
        // dictionary to map each node to its neighbours
        Dictionary<string, List<int>> neighboursD = new Dictionary<string, List<int>>();
        for (int i = 1; i <= parentA.Count; i++)
        {
            List<int> x = new List<int>();
            // find neighbours of 'i' (overflow at start and end)
            int indA = parentA.IndexOf(i);
            int indB = parentB.IndexOf(i);
            // there will be four neighbours
            int indAleft = indA > 0 ? indA - 1 : parentA.Count -1;
            int indAright = indA == parentA.Count -1 ? 0 : indA + 1;
            int indBleft = indB > 0 ? indB - 1 : parentB.Count -1;
            int indBright = indB == parentB.Count - 1 ? 0 : indB + 1;
            x.Add(parentA[indAleft]);
            x.Add(parentA[indAright]);
            // there might be duplicates at this point
            if (!x.Contains(parentB[indBleft]))
            {
                x.Add(parentB[indBleft]);
            }
            if (!x.Contains(parentB[indBright]))
            {
                x.Add(parentB[indBright]);
            }
            // add list x to neighbours
            neighboursD.Add(i.ToString(), x);
        }
        // choose one of the starting values
        int rando = Random.Range(0, 2);
        int workingNo = rando == 0 ? parentA[0] : parentB[0];
        offspring.Add(workingNo);
        // sort lists, and remove references to starting value
        foreach (KeyValuePair<string, List<int>> kvp in neighboursD)
        {
            kvp.Value.Sort();
            kvp.Value.RemoveAll(i => i == workingNo);
        }
        // start building offspring
        while (offspring.Count < parentA.Count)
        {
            int shortestlength = parentA.Count;
            int shortestpos = parentA.Count;
            
            if (neighboursD.ContainsKey(workingNo.ToString()))
            {
                // examine neighbours list, taking the neighbour value which in turn relates to the shortest list
                foreach (int i in neighboursD[workingNo.ToString()])
                {
                    if (neighboursD[i.ToString()].Count < shortestlength)
                    {
                        shortestpos = i;
                        shortestlength = neighboursD[i.ToString()].Count;
                    }
                }
            }
            // if we have exhausted the neighbour lists, we will start getting duplicates
            // or reference to empty lists, so check and generaate random value if so
            if (!offspring.Contains(shortestpos))
            {
                offspring.Add(shortestpos);
            }
            else
            {
                int randN = Random.Range(1, parentA.Count + 1);
                while (offspring.Contains(randN))
                {
                    randN = Random.Range(1, parentA.Count + 1);
                }
                shortestpos = randN;
                offspring.Add(shortestpos);
            }
            workingNo = shortestpos;
            // remove all references to chosen neighbour
            foreach (KeyValuePair<string, List<int>> kvp in neighboursD)
            {
                kvp.Value.RemoveAll(i => i == workingNo);
            }
        }
        return offspring;
    }
}
