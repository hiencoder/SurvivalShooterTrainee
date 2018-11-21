using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    //https://www.raywenderlich.com/847-object-pooling-in-unity
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;

}
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObject;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;

    }
    // Use this for initialization
    void Start()
    {
        pooledObject = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObject.Add(obj);
            }
        }
    }

    public GameObject getPoolObject(string tag)
    {
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy && pooledObject[i].tag == tag)
            {
                return pooledObject[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObject.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
