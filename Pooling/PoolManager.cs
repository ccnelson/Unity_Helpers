// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//Object Pool Manager
// Attach to empty object, assign pool parent objects to pools list, ensure each object has a name.
// Access this singleton from other scripts without a reference:
//
// private ObjectPooler somePool;
// public string somePoolName;
// somePool = PoolManager.poolManager.GetObjectPooler(somePoolName);
//
// This class uses 'Awake' to assign the pools. Therefore to avoid race conditions
// the above should take place after 'Awake', preferably in 'Start'
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager poolManager;

    [SerializeField] private List<GameObject> objectPools;
    private Dictionary<string, ObjectPooler> objectPoolDict;


    private void Awake()
    {
        poolManager = this;

        objectPoolDict = new Dictionary<string, ObjectPooler>();

        for (int i = 0; i < objectPools.Count; i++)
        {
            ObjectPooler objectPooler = objectPools[i].GetComponent<ObjectPooler>();
            objectPoolDict[objectPooler.poolName] = objectPooler;
        }
    }


    public ObjectPooler GetObjectPooler(string n)
    {
        return objectPoolDict[n];
    }
}
