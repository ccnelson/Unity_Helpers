// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Particle Object Pool Manager
// Attach to empty object, assign pool parent objects to pools list, ensure each object has a name.
// Access this singleton from other scripts without a reference:
//
// private ParticleObjectPooler somePool;
// public string somePoolName;
// somePool = PoolManager.poolManager.GetParticleObjectPooler(somePoolName);
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager poolManager;


    public List<GameObject> pools;
    public Dictionary<string, ParticleObjectPooler> poolDict;


    private void Awake()
    {
        poolManager = this;

        poolDict = new Dictionary<string, ParticleObjectPooler>();

        foreach (GameObject p in pools)
        {
            ParticleObjectPooler particlePooler = p.GetComponent<ParticleObjectPooler>();
            poolDict[particlePooler.poolName] = particlePooler;
        }
    }



    public ParticleObjectPooler GetParticleObjectPooler(string n)
    {
        return poolDict[n];
    }
}
