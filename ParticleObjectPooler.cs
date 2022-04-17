using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObjectPooler : MonoBehaviour
{
    public static ParticleObjectPooler sharedInstance;
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

    public GameObject GetFromPool(Vector3 pos, Quaternion rot)
    {
        for (int i = 0; i < _poolCount; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                _pool[i].transform.position = pos;
                _pool[i].transform.rotation = rot;
                _pool[i].SetActive(true);
                return _pool[i];
            }
        }
        return null;
    }
}
