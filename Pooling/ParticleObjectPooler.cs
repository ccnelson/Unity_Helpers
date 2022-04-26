// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// C NELSON 2022
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Object Pooler for particle systems.
// Assumes the paricles are set to 'Stop Action = Disable'
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleObjectPooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pool;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolCount;
    [SerializeField] public string poolName;
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }

    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PopulatePool();
    }


    private void Start()
    {
        PopulatePool();
    }


    private void PopulatePool()
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
