// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// ObjectPooler, for particle systems set to 'Stop Action = Disable'
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;
    [SerializeField] private List<GameObject> _pool;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolCount;


    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        _pool = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < _poolCount; i++)
        {
            tmp = Instantiate(_prefab);
            tmp.SetActive(false);
            _pool.Add(tmp);
        }
    }


    public void Spawn(Vector3 pos, Quaternion rot)
    {
        for (int i = 0; i < _poolCount; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                _pool[i].transform.position = pos;
                _pool[i].transform.rotation = rot;
                _pool[i].SetActive(true);
                return;
            }
        }
    }
}
