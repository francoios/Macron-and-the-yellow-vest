using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ObjectPoolItem
{
    [Tooltip("The model gameobject")]
    public GameObject ObjectToPool;

    [Tooltip("The number of gameobject to allocate initialy")]
    public uint InitialPoolSize;

    [Tooltip("Can the pool grow in size if needed")]
    public bool Expandable;
}

/// <summary>
/// Class managing gameobject that spawns a lot (bullets etc)
/// Instead of spawning and destroying an enormous amount of gameobjects reuse them over time
/// </summary>
public class ObjectPooler : MavMonoBehaviour
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance { get { return _instance; } }
    public List<ObjectPoolItem> ItemsToPool;

    private List<GameObject> _pooledObjects;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public override void Start()
    {
        StartCoroutine(InstantiateObjectOverTime());
        base.Start();
    }

    IEnumerator InstantiateObjectOverTime()
    {
        _pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in ItemsToPool)
        {
            for (int i = 0; i < item.InitialPoolSize; i++)
            {
                GameObject tmp = Instantiate(item.ObjectToPool, this.transform);
                tmp.SetActive(false);
                _pooledObjects.Add(tmp);
                if (i % 200 == 0)
                    yield return null;
            }
        }
        ScriptReady = true;
    }

    /// <summary>
    /// Get an available object in the pool and return it
    /// </summary>
    /// <returns>Return the requested gameobject.
    /// Return null if there is no gameobject available and expandable is set to false</returns>
    public GameObject GetPooledObject(string tag)
    {
        GameObject requestedObject = _pooledObjects.FirstOrDefault(x => !x.activeSelf && x.CompareTag(tag));
        if (requestedObject != null)
            return requestedObject;
        ObjectPoolItem spawningObject = ItemsToPool.Find(x => x.ObjectToPool.CompareTag(tag) && x.Expandable == true);
        if (spawningObject != null)
        {
            GameObject tmp = Instantiate(spawningObject.ObjectToPool);
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);
            return tmp;
        }
        return null;
    }

    public bool IsReady()
    {
        if (ScriptReady == true)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Return a list of x elements that are in the ObjectPooler
    /// </summary>
    /// <param name="tag">the tag of the GameObject in the PooledList</param>
    /// <param name="number">the number of element that will be in the returned list</param>
    /// <returns></returns>
    public IEnumerable<GameObject> GetPooledObject(string tag, int number)
    {
        IEnumerable<GameObject> list = _pooledObjects.Where(x => x.CompareTag(tag) && !x.activeSelf);
        ObjectPoolItem spawningObject = ItemsToPool.Find(x => x.ObjectToPool.CompareTag(tag) && x.Expandable == true);
        if (spawningObject != null && list.Count() < number)
        {
            int numToAdd = number - list.Count();
            for (int i = 0; i < numToAdd; i++)
            {
                GameObject tmp = Instantiate(spawningObject.ObjectToPool);
                tmp.SetActive(false);
                _pooledObjects.Add(tmp);
            }
        }
        list = _pooledObjects.Where(x => x.CompareTag(tag) && !x.activeSelf);
        IEnumerable<GameObject> gameObjects = list.Take(number);
        return gameObjects;
    }

}